using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.Service.Services
{
    public class AnswerService(IUnitOfWork _uow) : IAnswerService
    {
        public async Task<Result<PagedResult<DiagnosisAnswer>>> GetAnswersByDiagnosisId(Guid id)
        {
            if (id == Guid.Empty)
                return Result<PagedResult<DiagnosisAnswer>>.Failure("Diagnosis Id  cannot be empty.");

            var spec = BaseSpecification<DiagnosisAnswer>.Create()
                .Where(a => a.DeletedAt == null)
                .Include(a=>a.Question)
                .AndFilter(a => a.DiagnosisId == id);

            return Result<PagedResult<DiagnosisAnswer>>.Success(await _uow.AnswerRepository.PagedListAsync(spec));
        }

        public async Task<Result<Guid>> SaveDiagnosisAnswersAsync(Guid diagnosisId, List<AnswerRequestDto> answers)
        {
            if (diagnosisId == Guid.Empty)

                return Result<Guid>.Failure("Diagnosis Id ID cannot be empty.");

            if (answers == null || !answers.Any())
                return Result<Guid>.Failure("قائمة الاجابات فارغة");

            var diagnosisQuestions = await _uow.QuestionRepository.GetDiagnosisQuestionsAsync();
            var questionIds = diagnosisQuestions.Select(q => q.Id).ToList();

            var oldAnswers = await _uow.AnswerRepository
                .FindAllAsync(a => a.DiagnosisId == diagnosisId && questionIds.Contains(a.QuestionId));

            foreach (var old in oldAnswers)
            {
                old.DeletedAt = DateTime.UtcNow;
            }

            foreach (var answerDto in answers)
            {
                var answer = new DiagnosisAnswer
                {
                    Id = Guid.NewGuid(),
                    DiagnosisId = diagnosisId,
                    QuestionId = answerDto.QuestionId,
                    BooleanValue = answerDto.BooleanValue,
                    ScoreValue = answerDto.ScoreValue,
                    TextValue = answerDto.TextValue,
                    SelectedOption = answerDto.SelectedOption,
                    Notes = answerDto.Notes
                };
                await _uow.AnswerRepository.AddAsync(answer);
            }

            await _uow.CompleteAsync();
            return Result<Guid>.Success(diagnosisId);
        }

        public async Task<Result<EvaluationCard>> SaveAndEvaluateCardAsync(EvaluateCardRequestDto dto)
        {
            var allQuestions = await _uow.QuestionRepository.GetAllAsync();

            var cardQuestions = allQuestions
                .Where(q => q.CardName == dto.CardName && q.DeletedAt == null)
                .ToList();

            if (!cardQuestions.Any())
            {
                var evaluation = new EvaluationCard
                {
                    Id = Guid.NewGuid(),
                    DiagnosisId = dto.DiagnosisId,
                    CardName = dto.CardName,
                    MainTitleScores = new Dictionary<string, int>(),
                    TotalScore = 0,
                    EvaluationMessage = "لا توجد أسئلة لهذه البطاقة"
                };
                return Result<EvaluationCard>.Success(evaluation);
            }


            var questionIds = cardQuestions.Select(q => q.Id).ToList();

            
            var oldAnswers = await _uow.AnswerRepository
                .FindAllAsync(a => a.DiagnosisId == dto.DiagnosisId && questionIds.Contains(a.QuestionId));

            if(oldAnswers.Any())
            {
                foreach (var old in oldAnswers)
                {
                    old.DeletedAt = DateTime.UtcNow;
                }

            }
            
            foreach (var answerDto in dto.Answers)
            {
                var answer = new DiagnosisAnswer
                {
                    Id = Guid.NewGuid(),
                    DiagnosisId = dto.DiagnosisId,
                    QuestionId = answerDto.QuestionId,
                    BooleanValue = answerDto.BooleanValue,
                    ScoreValue = answerDto.ScoreValue,
                    TextValue = answerDto.TextValue,
                    SelectedOption = answerDto.SelectedOption,
                    Notes = answerDto.Notes
                };
                await _uow.AnswerRepository.AddAsync(answer);
            }

            await _uow.CompleteAsync();

            var savedAnswers = await _uow.AnswerRepository
                .FindAllAsync(a => a.DiagnosisId == dto.DiagnosisId && questionIds.Contains(a.QuestionId));

            var calculatedResult = await CalculateEvaluationForCard(savedAnswers.ToList(), dto.CardName);

            var oldResult = await _uow.EvaluationCardRepository
                .GetByItemAsync(e => e.DiagnosisId == dto.DiagnosisId && e.CardName == dto.CardName);

            if (oldResult != null)
            {
                oldResult.DeletedAt = DateTime.UtcNow;
            }

            var evaluationResult = new EvaluationCard
            {
                Id = Guid.NewGuid(),
                DiagnosisId = dto.DiagnosisId,
                CardName = dto.CardName,
                MainTitleScores = calculatedResult.Value.MainTitleScores,
                TotalScore = calculatedResult.Value.TotalScore,
                EvaluationMessage = calculatedResult.Value.EvaluationMessage
            };

            await _uow.EvaluationCardRepository.AddAsync(evaluationResult);
            await _uow.CompleteAsync();

            return Result<EvaluationCard>.Success(calculatedResult.Value);
        }

        public async Task<Result<EvaluationCard>> CalculateEvaluationForCard(List<DiagnosisAnswer> answers, string cardName)
        {
            var result = new EvaluationCard
            {
                Id = Guid.NewGuid(),
                CardName = cardName,
                MainTitleScores = new Dictionary<string, int>(),
                TotalScore = 0,
                EvaluationMessage = string.Empty
            };


            // 1. فلترة الأسئلة اللي تخص هذه البطاقة الرئيسي
            var spec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardName == cardName)
                .OrderByAsc(q => q.Order);

            var relevantQuestions = await _uow.QuestionRepository.ListAsync(spec);

            if (!relevantQuestions.Any())
            {
                result.EvaluationMessage = "لا توجد أسئلة لهذا التقييم";
                return Result<EvaluationCard>.Success(result);
            }

            // 2. تجميع الأسئلة حسب العنوان الرئيسي (MainTitle)
            var grouped = relevantQuestions
                .GroupBy(q => q.MainTitle ?? "بدون عنوان رئيسي")
                .ToList();

            // 3. حساب المجموع لكل عنوان رئيسي
            foreach (var group in grouped)
            {
                string mainTitle = group.Key;
                int total = 0;

                foreach (var question in group)
                {
                    var answer = answers.FirstOrDefault(a => a.QuestionId == question.Id);
                     
                    // فقط الأسئلة من نوع Score تدخل في الحساب
                    if (question.Type == QuestionType.Score)
                    {
                        total += answer?.ScoreValue ?? 0;
                    }
                }

                var titleScoreDict = new Dictionary<string, int>
                {
                      { mainTitle, total }
                };

                result.MainTitleScores[mainTitle] = total;
                result.TotalScore += total;
            }

            result.EvaluationMessage = GetEvaluationMessageByCardName(cardName, result.TotalScore);

            return Result<EvaluationCard>.Success(result);
        }
        private static string GetEvaluationMessageByCardName(string cardName, int totalScore)
        {
            switch (cardName)
            {
                case "بطاقة التقييم الخاصة بالأسئلة الموجهة للأهل":
                    return GetParentQuestionnaireMessage(totalScore);

                case "بطاقة التقييم للأطفال أقل من 7 سنوات":
                    return GetUnderSevenYearsMessage(totalScore);

                case "بطاقة التقييم للأطفال 7 سنوات فما فوق":
                    return GetAboveSevenYearsMessage(totalScore);

                default:
                    return "تم حساب التقييم بنجاح";
            }
        }

        private static string GetParentQuestionnaireMessage(int totalScore)
        {
            if (totalScore >= 60 && totalScore <= 75)
                return "التقييم: ضعيف";
            else if (totalScore >= 50 && totalScore <= 59)
                return "التقييم: وسط";
            else if (totalScore >= 25 && totalScore <= 49)
                return "التقييم: جيد";

            return "التقييم: جيد جداً";

        }

        private static string GetUnderSevenYearsMessage(int totalScore)
        {
            if (totalScore >= 15)
                return "✅ الطفل مؤهل للصفوف التعليمية التابعة للمشروع";
            else
                return $"❌ الطفل غير مؤهل للصفوف التعليمية - يجب أن يحقق 15 درجة كحد أدنى (حقق {totalScore} درجة)";
        }

        private static string GetAboveSevenYearsMessage(int totalScore)
        {
            if (totalScore >= 18)
                return "✅ الطفل مؤهل للصفوف التعليمية التابعة للمشروع";
            else
                return $"❌ الطفل غير مؤهل للصفوف التعليمية - يجب أن يحقق 18 درجة كحد أدنى )حقق {totalScore} درجة)";
        }



    }
}


