using FluentValidation;
using Microsoft.OpenApi.Extensions;
using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.Dtos.DiagnisisAnswerDto.Requests;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Responses;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Answer;

namespace Qandil.Service.Services
{
    public class DiagnisisAnswerService(IUnitOfWork _uow) : IAnswerService
    {

        private const string DiagnosisMainTitle = "أسئلة التشخيص";

        // ============ 1. حفظ إجابات بطاقة معينة وإرجاع نتيجتها المحسوبة ============
        public async Task<Result<CardResultDto>> SaveAndEvaluateCardAsync(EvaluateCardRequestDto dto)
        {
            if (dto.DiagnosisId == Guid.Empty)
                return Result<CardResultDto>.Failure("معرف التشخيص غير صالح");

            if (dto.CardType == CardType.None)
                return Result<CardResultDto>.Failure("يرجى تحديد نوع البطاقة");

            if (dto.Answers == null || !dto.Answers.Any())
                return Result<CardResultDto>.Failure("قائمة الإجابات فارغة");
            await new SaveCardAnsweValidator(_uow).ValidateAndThrowAsync(dto);

            // 1.1 جلب أسئلة هاي البطاقة بس
            var questionsSpec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardType == dto.CardType)
                .OrderByAsc(q => q.Order);

            var cardQuestions = await _uow.DiagnosisQuestionRepository.ListAsync(questionsSpec);

            if (!cardQuestions.Any())
                return Result<CardResultDto>.Failure("لا توجد أسئلة لهذه البطاقة");

            var questionIds = cardQuestions.Select(q => q.Id).ToList();

            // 1.2 Soft delete لإجابات سابقة لنفس البطاقة (لو المستخدم عم يعدّل)
            var oldAnswersSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == dto.DiagnosisId)
                .AndFilter(a => questionIds.Contains(a.QuestionId));

            var oldAnswers = await _uow.AnswerRepository.ListAsync(oldAnswersSpec);
            foreach (var old in oldAnswers)
                old.DeletedAt = DateTime.UtcNow;

            // 1.3 حفظ الإجابات الجديدة
            foreach (var answerDto in dto.Answers)
            {
                var answer = new DiagnosisAnswer
                {
                    Id = Guid.NewGuid(),
                    DiagnosisId = dto.DiagnosisId,
                    QuestionId = answerDto.QuestionId,
                    ScoreValue = answerDto.ScoreValue,
                    answerFrequency=answerDto.answerFrequency,
                    Notes = answerDto.Notes,
                    CreatedAt = DateTime.UtcNow
                };
                await _uow.AnswerRepository.AddAsync(answer);
            }

            await _uow.CompleteAsync();

            // 1.4 إعادة جلب الإجابات المحفوظة فقط، وحساب النتيجة مباشرة (بدون تخزين)
            var savedAnswersSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == dto.DiagnosisId)
                .AndFilter(a => questionIds.Contains(a.QuestionId));

            var savedAnswers = await _uow.AnswerRepository.ListAsync(savedAnswersSpec);

            var result = CalculateEvaluationForCard(savedAnswers.ToList(), cardQuestions.ToList(), dto.CardType);

            return Result<CardResultDto>.Success(result);
        }

        // حساب النتيجة (تجميع حسب SubTitle) — Pure function، بدون أي DB call
        private static CardResultDto CalculateEvaluationForCard(
            List<DiagnosisAnswer> answers,
            List<DiagnosisQuestion> relevantQuestions,
            CardType cardType)
        {
            var result = new CardResultDto
            {
                CardType = cardType,
                DisplayName = cardType.GetDisplayName(),
                SubTitleScores = new Dictionary<string, int>(),
                TotalScore = 0
            };

            var grouped = relevantQuestions.GroupBy(q => q.SubTitle ?? "بدون عنوان");

            foreach (var group in grouped)
            {
                int total = 0;
                foreach (var question in group)
                {
                    var answer = answers.FirstOrDefault(a => a.QuestionId == question.Id);
                    total += answer?.ScoreValue ?? 0;
                }

                result.SubTitleScores[group.Key] = total;
                result.TotalScore += total;
            }

            result.EvaluationMessage = GetEvaluationMessageByCardType(cardType, result.TotalScore);
            return result;
        }

        private static string GetEvaluationMessageByCardType(CardType cardType, int totalScore)
        {
            return cardType switch
            {
                CardType.ParentEvaluation => GetParentQuestionnaireMessage(totalScore),
                CardType.Child4To7Years => GetUnderSevenYearsMessage(totalScore),
                CardType.ChildAbove7Years => GetAboveSevenYearsMessage(totalScore),
                CardType.PreviouslyDiagnosed => GetReEvaluationMessage(totalScore), // ⚠️ لازم معاييرها الفعلية
                _ => "تم حساب التقييم بنجاح"
            };
        }

        private static string GetParentQuestionnaireMessage(int totalScore)
        {
            if (totalScore >= 60 && totalScore <= 75) return "التقييم: ضعيف";
            if (totalScore >= 50 && totalScore <= 59) return "التقييم: وسط";
            if (totalScore >= 25 && totalScore <= 49) return "التقييم: جيد";
            return "التقييم: جيد جداً";
        }

        private static string GetUnderSevenYearsMessage(int totalScore)
        {
            return totalScore >= 15
                ? "✅ الطفل مؤهل للصفوف التعليمية التابعة للمشروع"
                : $"❌ الطفل غير مؤهل للصفوف التعليمية - يجب أن يحقق 15 درجة كحد أدنى (حقق {totalScore} درجة)";
        }

        private static string GetAboveSevenYearsMessage(int totalScore)
        {
            return totalScore >= 18
                ? "✅ الطفل مؤهل للصفوف التعليمية التابعة للمشروع"
                : $"❌ الطفل غير مؤهل للصفوف التعليمية - يجب أن يحقق 18 درجة كحد أدنى (حقق {totalScore} درجة)";
        }

        private static string GetReEvaluationMessage(int totalScore)
        {
            // ⚠️ حط هون معايير بطاقة إعادة التقييم الفعلية عندك
            return $"تم حساب إعادة التقييم - المجموع: {totalScore}";
        }

        public async Task<Result<Guid>> SaveDiagnosisSubTitleAnswersAsync(SaveDiagnosisSubTitleAnswersRequestDto dto)
        {
            if (dto.DiagnosisId == Guid.Empty)
                return Result<Guid>.Failure("معرف التشخيص غير صالح");

            if (string.IsNullOrWhiteSpace(dto.SubTitle))
                return Result<Guid>.Failure("العنوان الفرعي مطلوب");

            if (dto.Answers == null || !dto.Answers.Any())
                return Result<Guid>.Failure("قائمة الإجابات فارغة");

            var questionsSpec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.MainTitle == DiagnosisMainTitle)
                .AndFilter(q => q.SubTitle == dto.SubTitle);

            var groupQuestions = await _uow.DiagnosisQuestionRepository.ListAsync(questionsSpec);

            if (!groupQuestions.Any())
                return Result<Guid>.Failure("لا توجد أسئلة لهذا العنوان الفرعي");

            var questionIds = groupQuestions.Select(q => q.Id).ToList();

            var oldAnswersSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == dto.DiagnosisId)
                .AndFilter(a => questionIds.Contains(a.QuestionId));

            var oldAnswers = await _uow.AnswerRepository.ListAsync(oldAnswersSpec);
            foreach (var old in oldAnswers)
                old.DeletedAt = DateTime.UtcNow;

            foreach (var answerDto in dto.Answers)
            {
                var answer = new DiagnosisAnswer
                {
                    Id = Guid.NewGuid(),
                    DiagnosisId = dto.DiagnosisId,
                    QuestionId = answerDto.QuestionId,
                    BooleanValue = answerDto.BooleanValue,
                    TextValue = answerDto.TextValue,
                    SelectedOption = answerDto.SelectedOption,
                    Notes = answerDto.Notes,
                    CreatedAt = DateTime.UtcNow
                };
                await _uow.AnswerRepository.AddAsync(answer);
            }

            await _uow.CompleteAsync();
            return Result<Guid>.Success(dto.DiagnosisId);
        }

        public async Task<Result<List<CardResultDto>>> GetCardResultsAsync(Guid diagnosisId)
        {
            if (diagnosisId == Guid.Empty)
                return Result<List<CardResultDto>>.Failure("معرف التشخيص غير صالح");

            // 3.1 جلب كل أسئلة البطاقات (Type == Score، أي عندها CardType محدد)
            var questionsSpec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardType.HasValue);

            var allCardQuestions = await _uow.DiagnosisQuestionRepository.ListAsync(questionsSpec);

            if (!allCardQuestions.Any())
                return Result<List<CardResultDto>>.Success(new List<CardResultDto>());

            var allQuestionIds = allCardQuestions.Select(q => q.Id).ToList();

            // 3.2 جلب كل إجابات هاد الطفل على أسئلة البطاقات
            var answersSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == diagnosisId)
                .AndFilter(a => allQuestionIds.Contains(a.QuestionId));

            var allAnswers = await _uow.AnswerRepository.ListAsync(answersSpec);

            if (!allAnswers.Any())
                return Result<List<CardResultDto>>.Success(new List<CardResultDto>());

            // 3.3 تحديد البطاقات يلي فعلاً فيها إجابات (يعني تمت تعبئتها)
            var answeredQuestionIds = allAnswers.Select(a => a.QuestionId).ToHashSet();
            var relevantCardTypes = allCardQuestions
                .Where(q => answeredQuestionIds.Contains(q.Id))
                .Select(q => q.CardType!.Value)
                .Distinct()
                .ToList();

            // 3.4 حساب نتيجة كل بطاقة تمت الإجابة عليها
            var results = new List<CardResultDto>();
            foreach (var cardType in relevantCardTypes)
            {
                var cardQuestions = allCardQuestions.Where(q => q.CardType == cardType).ToList();
                var cardAnswers = allAnswers
                    .Where(a => cardQuestions.Select(q => q.Id).Contains(a.QuestionId))
                    .ToList();

                results.Add(CalculateEvaluationForCard(cardAnswers, cardQuestions, cardType));
            }

            return Result<List<CardResultDto>>.Success(results);
        }
        public async Task<Result<CardDetailsResponseDto>> GetCardAnswerDetailsAsync(Guid diagnosisId, CardType cardType)
        {
            if (diagnosisId == Guid.Empty)
                return Result<CardDetailsResponseDto>.Failure("معرف التشخيص غير صالح");

            var questionsSpec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardType == cardType)
                .OrderByAsc(q => q.Order);

            var questions = await _uow.DiagnosisQuestionRepository.ListAsync(questionsSpec);

            if (!questions.Any())
                return Result<CardDetailsResponseDto>.Failure("لا توجد أسئلة لهذه البطاقة");

            var questionIds = questions.Select(q => q.Id).ToList();

            var answersSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == diagnosisId)
                .AndFilter(a => questionIds.Contains(a.QuestionId));

            var answers = await _uow.AnswerRepository.ListAsync(answersSpec);

            var details = questions.Select(q =>
            {
                var answer = answers.FirstOrDefault(a => a.QuestionId == q.Id);
                var scoreValue = answer?.ScoreValue;

                string displayAnswer = q.ScoreInputType == ScoreInputType.Frequency
                    ? (scoreValue.HasValue ? ((AnswerFrequency)scoreValue.Value).ToString() : "لم تتم الإجابة")
                    : (scoreValue?.ToString() ?? "لم تتم الإجابة");

                return new CardAnswerDetailDto
                {
                    
                    AnswerId = answer?.Id,
                    QuistionId=q.Id,
                    QuestionText = q.QuestionText,
                    SubTitle = q.SubTitle,
                    ScoreValue = scoreValue,
                    DisplayAnswer = displayAnswer
                };
            }).ToList();

            return Result<CardDetailsResponseDto>.Success(new CardDetailsResponseDto
            {
                CardType = cardType,
                DisplayName = cardType.GetDisplayName(),
                Answers = details
            });
        }


        public async Task<Result<List<DiagnosisSubTitleResultDto>>> GetDiagnosisQuestionsResultsAsync(Guid diagnosisId)
        {
            if (diagnosisId == Guid.Empty)
                return Result<List<DiagnosisSubTitleResultDto>>.Failure("معرف التشخيص غير صالح");

            var questionsSpec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.MainTitle == DiagnosisMainTitle)
                .Include(q => q.QuestionOptions)
                .OrderByAsc(q => q.SubTitle)
                .OrderByAsc(q => q.Order);

            var questions = await _uow.DiagnosisQuestionRepository.ListAsync(questionsSpec);

            if (!questions.Any())
                return Result<List<DiagnosisSubTitleResultDto>>.Success(new List<DiagnosisSubTitleResultDto>());

            var questionIds = questions.Select(q => q.Id).ToList();

            var answersSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == diagnosisId)
                .AndFilter(a => questionIds.Contains(a.QuestionId));

            var answers = await _uow.AnswerRepository.ListAsync(answersSpec);

            var groups = questions
                .GroupBy(q => q.SubTitle ?? "بدون عنوان")
                .Select(group => new DiagnosisSubTitleResultDto
                {
                    SubTitle = group.Key,
                    Answers = group.OrderBy(q => q.Order).Select(q =>
                    {
                        var answer = answers.FirstOrDefault(a => a.QuestionId == q.Id);

                        string selectedOptionText = null;
                        if (q.Type == QuestionType.Options && answer?.SelectedOption != null)
                        {
                            selectedOptionText = answer.SelectedOption;

                        }

                        return new DiagnosisAnswerDetailDto
                        {
                            AnswerId = answer?.Id,
                            QuestionId = q.Id,
                            QuestionText = q.QuestionText,
                            Type = q.Type,
                            BooleanValue = answer?.BooleanValue,
                            TextValue = answer?.TextValue,
                            SelectedOptionText = selectedOptionText
                        };
                    }).ToList()
                }).ToList();

            return Result<List<DiagnosisSubTitleResultDto>>.Success(groups);
        }

        public async Task<Result<bool>> UpdateAnswerAsync(Guid answerId, UpdateAnswerRequestDto dto)
        {
            if (answerId == Guid.Empty)
                return Result<bool>.Failure("معرف الإجابة غير صالح");

            var answer = await _uow.AnswerRepository.GetByIdAsync(answerId);

            if (answer == null || answer.DeletedAt != null)
                return Result<bool>.Failure("الإجابة غير موجودة");

            answer.ScoreValue = dto.ScoreValue;
            answer.BooleanValue = dto.BooleanValue;
            answer.TextValue = dto.TextValue;
            answer.SelectedOption = dto.SelectedOption;
            answer.Notes = dto.Notes;


            await _uow.AnswerRepository.UpdateAsync(answer);
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
        public async Task<Result<bool>> DeleteAnswerByQuestionAsync(Guid diagnosisId, Guid questionId)
        {
            if (diagnosisId == Guid.Empty)
                return Result<bool>.Failure("معرف التشخيص غير صالح");

            if (questionId == Guid.Empty)
                return Result<bool>.Failure("معرف السؤال غير صالح");

            var answerSpec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .AndFilter(a => a.DiagnosisId == diagnosisId)
                .AndFilter(a => a.QuestionId == questionId);

            var answer = (await _uow.AnswerRepository.ListAsync(answerSpec)).FirstOrDefault();

            if (answer == null)
                return Result<bool>.Failure("لا توجد إجابة لهذا السؤال ضمن هذا التشخيص");

            answer.DeletedAt = DateTime.UtcNow;

            await _uow.AnswerRepository.UpdateAsync(answer);
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

    }
}


