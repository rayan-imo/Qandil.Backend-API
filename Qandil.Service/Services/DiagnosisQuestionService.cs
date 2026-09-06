using FluentValidation;
using Microsoft.OpenApi.Extensions;
using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Requests;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Responses;
using Qandil.Service.Dtos.QuestionOptionsDto.Responses;
using Qandil.Service.IServices;
using Qandil.Service.Validation.DiagnosisQuestions;

namespace Qandil.Service.Services
{
    public class DiagnosisQuestionService(IUnitOfWork _uow) : IDiagnosisQuestionService
    {


        public async Task<Result<List<DiagnosisQuestionResponseDto>>> GetAllDiagnosisQuestionsAsync()
        {
            // 1. جلب كل أسئلة التشخيص من قاعدة البيانات
            var spec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.DeletedAt == null)
                .AndFilter(x => x.MainTitle == "أسئلة التشخيص")
                .Include(x => x.QuestionOptions)
                .OrderByAsc(x => x.SubTitle)
                .OrderByAsc(x => x.Order);

            var questions = await _uow.DiagnosisQuestionRepository.ListAsync(spec);

            if (questions == null || !questions.Any())
                return Result<List<DiagnosisQuestionResponseDto>>.Success(new List<DiagnosisQuestionResponseDto>());


            var groups = questions
                .GroupBy(q => q.SubTitle ?? "بدون عنوان")
                .Select(group => new DiagnosisQuestionResponseDto
                {
                    SubTitle = group.Key,  // ← العنوان الفرعي
                    Questions = group
                        .OrderBy(q => q.Order)
                        .Select(q => new DiagnosisQuestionDto
                        {
                            Id = q.Id,
                            QuestionText = q.QuestionText,
                            Type = q.Type,
                            Order = q.Order,
                            Options = q.QuestionOptions?
                                .Where(o => o.DeletedAt == null)
                                .OrderBy(o => o.Order)
                                .Select(o => new DiagnosisOptionDto
                                {
                                    Id = o.Id,
                                    Text = o.Text,
                                }).ToList() ?? new List<DiagnosisOptionDto>()
                        }).ToList()
                }).ToList();

            return Result<List<DiagnosisQuestionResponseDto>>.Success(groups);
        }

        public async Task<Result<List<CardQuestionsResponseDto>>> GetAllCardQuestionsAsync()
        {
            var spec = BaseSpecification<DiagnosisQuestion>.Create()
        .Where(x => x.DeletedAt == null)
        .AndFilter(x => x.CardType != null)
        .OrderByDesc(x => x.CardType)
        .OrderByDesc(x => x.SubTitle)
        .OrderByDesc(x => x.Order);

            var questions = await _uow.DiagnosisQuestionRepository.ListAsync(spec);

            if (questions == null || !questions.Any())
                return Result<List<CardQuestionsResponseDto>>.Success(new List<CardQuestionsResponseDto>());

            var cards = questions
                .GroupBy(q => q.CardType)
                .Select(cardGroup => new CardQuestionsResponseDto
                {
                    CardType = cardGroup.Key,
                    DisplayName = cardGroup.Key.GetDisplayName(),
                    SubTitleGroups = cardGroup
                        .GroupBy(q => q.SubTitle ?? "بدون عنوان")
                        .Select(subGroup => new SubTitleGroupDto
                        {
                            SubTitle = subGroup.Key,
                            Questions = subGroup
                                .OrderBy(q => q.Order)
                                .Select(q => new QuestionDto
                                {
                                    Id = q.Id,
                                    QuestionText = q.QuestionText,
                                    Type = q.Type,
                                    ScoreInputType = q.ScoreInputType,
                                    MinValue = q.MinValue,
                                    MaxValue = q.MaxValue
                                }).ToList()
                        }).ToList()
                }).ToList();

            return Result<List<CardQuestionsResponseDto>>.Success(cards);
        }
        public async Task<Result<List<DiagnosisQuestion>>> GetQuestionsByCardType(CardType cardType)
        {

            var spec = BaseSpecification<DiagnosisQuestion>.Create()

                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardType == cardType)
                .OrderByAsc(q => q.Order);

            var cardQuestions = await _uow.DiagnosisQuestionRepository.ListAsync(spec);

            if (!cardQuestions.Any())

                return Result<List<DiagnosisQuestion>>.Failure($"لا يوجد اسئلة ل {cardType.GetDisplayName()}");

            return Result<List<DiagnosisQuestion>>.Success(cardQuestions);

        }
        public async Task<Result<DiagnosisQuestion>> GetQuestionByIdAsync(Guid id)
        {
            var question = await _uow.DiagnosisQuestionRepository.GetByIdAsync(id);

            if (question == null)
                return Result<DiagnosisQuestion>.Failure("السؤال غير موجود");

            return Result<DiagnosisQuestion>.Success(question);
        }

        public async Task<Result<DiagnosisQuestion>> AddCardQuestionAsync(CardQuestionRequestDto dto)
        {
            await new CardQuestionAddValidator().ValidateAndThrowAsync(dto);

            var spec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.DeletedAt == null)
                .AndFilter(x => x.CardType == dto.CardType)
                .AndFilter(x => x.QuestionText == dto.QuestionText);

            var existingQuestion = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(spec);
            if (existingQuestion != null)
                return Result<DiagnosisQuestion>.Failure("يوجد بالفعل سؤال بنفس النص في هذه البطاقة");

            var scoreInputType = CardScoreInputTypeResolver.Resolve(dto.CardType);   // ← تلقائي حسب البطاقة

            var question = new DiagnosisQuestion
            {
                Id = Guid.NewGuid(),
                CardType = dto.CardType,
                SubTitle = dto.SubTitle,
                QuestionText = dto.QuestionText,
                Type = QuestionType.Score,
                ScoreInputType = scoreInputType,
                MinValue = scoreInputType == ScoreInputType.RawNumber ? dto.MinValue : null,
                MaxValue = scoreInputType == ScoreInputType.RawNumber ? dto.MaxValue : null,
                Order = dto.Order,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.DiagnosisQuestionRepository.AddAsync(question);
            await _uow.CompleteAsync();

            return Result<DiagnosisQuestion>.Success(question);
        }

        public async Task<Result<DiagnosisQuestion>> UpdateCardQuestionAsync(Guid id, CardQuestionRequestDto dto)
        {
            if (id == Guid.Empty)
                return Result<DiagnosisQuestion>.Failure("معرف السؤال غير صالح");

            await new CardQuestionAddValidator().ValidateAndThrowAsync(dto);

            var spec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.DeletedAt == null)
                .AndFilter(x => x.Id == id);

            var question = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(spec);

            if (question == null)
                return Result<DiagnosisQuestion>.Failure("السؤال غير موجود");

            var scoreInputType = CardScoreInputTypeResolver.Resolve(dto.CardType);

            question.CardType = dto.CardType;
            question.SubTitle = dto.SubTitle;
            question.QuestionText = dto.QuestionText;
            question.ScoreInputType = scoreInputType;
            question.MinValue = scoreInputType == ScoreInputType.RawNumber ? dto.MinValue : null;
            question.MaxValue = scoreInputType == ScoreInputType.RawNumber ? dto.MaxValue : null;
            question.Order = dto.Order;

            await _uow.DiagnosisQuestionRepository.UpdateAsync(question);
            await _uow.CompleteAsync();

            return Result<DiagnosisQuestion>.Success(question);
        }
        public async Task<Result<DiagnosisQuestion>> AddDiagnosisQuestionAsync(DiagnosisQuestionRequestDto dto)
        {
            // 1.1 التحقق من صحة البيانات
            await new DiagnosisQuestionValidator().ValidateAndThrowAsync(dto);

            // 1.2 التحقق من عدم وجود سؤال مكرر
            var spec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.DeletedAt == null)
                .AndFilter(q => q.MainTitle == "أسئلة التشخيص")
                .AndFilter(q => q.QuestionText == dto.QuestionText);

            var existingQuestion = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(spec);
            if (existingQuestion != null)
                return Result<DiagnosisQuestion>.Failure($"يوجد بالفعل سؤال بنفس النص في هذا العنوان");


            var question = new DiagnosisQuestion
            {
                Id = Guid.NewGuid(),
                MainTitle = "أسئلة التشخيص",
                SubTitle = dto.SubTitle,
                QuestionText = dto.QuestionText,
                Type = dto.Type,
                Order = dto.Order,
                CreatedAt = DateTime.UtcNow
            };


            if (dto.Type == QuestionType.Options && dto.Options != null && dto.Options.Any())
            {
                foreach (var optionDto in dto.Options)
                {
                    var option = new QuestionOption
                    {
                        Id = Guid.NewGuid(),
                        Text = optionDto.Text,
                        Order = optionDto.Order,
                        DiagnosisQuestionId = question.Id,
                        CreatedAt = DateTime.UtcNow
                    };
                    question.QuestionOptions.Add(option);
                }
            }


            await _uow.DiagnosisQuestionRepository.AddAsync(question);
            await _uow.CompleteAsync();


            var specWithInclude = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.Id == question.Id && x.DeletedAt == null)
                .Include(x => x.QuestionOptions);

            var questionWithOptions = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(specWithInclude);


            return Result<DiagnosisQuestion>.Success(questionWithOptions);
        }
        public async Task<Result<DiagnosisQuestion>> UpdateDiagnosisQuestionAsync(Guid id, DiagnosisQuestionRequestDto dto)
        {

            if (id == Guid.Empty)
                return Result<DiagnosisQuestion>.Failure("معرف السؤال غير صالح");


            // await new DiagnosisQuestionValidator().ValidateAndThrowAsync(dto);


            var spec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.DeletedAt == null)
                .AndFilter(x => x.Id == id)
                .Include(x => x.QuestionOptions);

            var question = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(spec);

            if (question == null)
                return Result<DiagnosisQuestion>.Failure($"السؤال غير موجود");

            question.SubTitle = dto.SubTitle;
            question.QuestionText = dto.QuestionText;
            question.Type = dto.Type;
            question.Order = dto.Order;

            // 5. تحديث الخيارات
            // 5.1 حذف الخيارات القديمة (Soft Delete)
            if (question.QuestionOptions != null && question.QuestionOptions.Any())
            {
                foreach (var oldOption in question.QuestionOptions)
                {
                    oldOption.DeletedAt = DateTime.UtcNow;
                }
            }
            if (dto.Type == QuestionType.Options &&
                   dto.Options != null &&
                   dto.Options.Any())
            {
                foreach (var optionDto in dto.Options)
                {
                    var option = new QuestionOption
                    {
                        Id = Guid.NewGuid(),
                        Text = optionDto.Text,
                        Order = optionDto.Order,
                        DiagnosisQuestionId = question.Id,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _uow.QuestionOptionRepository.AddAsync(option);
                }
            }

          //  await _uow.DiagnosisQuestionRepository.UpdateAsync(question);
            await _uow.CompleteAsync();


            var specWithInclude = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(x => x.Id == question.Id && x.DeletedAt == null)
                .Include(x => x.QuestionOptions);

            var updatedQuestion = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(specWithInclude);


            return Result<DiagnosisQuestion>.Success(updatedQuestion);
        }
        public async Task<Result<bool>> DeleteQuestionAsync(Guid id)
        {
            var spec = BaseSpecification<DiagnosisQuestion>.Create()
        .Where(x => x.DeletedAt == null && x.Id == id)
        .Include(x => x.QuestionOptions);  // ← أضف Include للخيارات

            var question = await _uow.DiagnosisQuestionRepository.GetFirstBySpecAsync(spec);

            if (question == null)
                return Result<bool>.Failure("السؤال غير موجود");

            // Soft Delete للسؤال
            question.DeletedAt = DateTime.UtcNow;

            // Soft Delete للخيارات
            if (question.QuestionOptions != null && question.QuestionOptions.Any())
            {
                foreach (var option in question.QuestionOptions)
                {
                    option.DeletedAt = DateTime.UtcNow;
                }
            }

            await _uow.DiagnosisQuestionRepository.UpdateAsync(question);
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }




    }
}
