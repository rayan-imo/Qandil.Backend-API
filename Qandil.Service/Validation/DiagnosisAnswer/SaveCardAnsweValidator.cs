
using FluentValidation;
using FluentValidation.Validators;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.AnswerDto.Requests;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Qandil.Service.Validation.Answer
{
    public class SaveCardAnsweValidator : AbstractValidator<EvaluateCardRequestDto>
    {
        private readonly IUnitOfWork _uow;

        public SaveCardAnsweValidator(IUnitOfWork uow)
        {
            _uow = uow;

            RuleFor(x => x.DiagnosisId)
                .NotEmpty()
                .WithMessage("معرف التشخيص غير صالح");

            RuleFor(x => x.CardType)
                .IsInEnum()
                .WithMessage("نوع البطاقة غير صالح")
                .Must(ct => ct != CardType.None)
                .WithMessage("يرجى تحديد نوع البطاقة");

            RuleFor(x => x.Answers)
                .NotEmpty()
                .WithMessage("قائمة الإجابات فارغة");

            RuleForEach(x => x.Answers)
                .SetValidator(new AnswerRequestValidator());

            RuleFor(x => x)
                .CustomAsync(async (dto, context, cancellationToken) =>
                {
                    await ValidateAnswersMatchQuestionTypesAsync(
                        dto,
                        context,
                        cancellationToken);
                });
        }

        private async Task ValidateAnswersMatchQuestionTypesAsync(
            EvaluateCardRequestDto dto,
            CustomContext context,
            CancellationToken cancellationToken)
        {
            if (dto.CardType == CardType.None ||
                dto.Answers == null ||
                !dto.Answers.Any())
            {
                return;
            }

            var questionsSpec = BaseSpecification<DiagnosisQuestion>.Create()
                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardType == dto.CardType);

            var cardQuestions =
                await _uow.DiagnosisQuestionRepository.ListAsync(questionsSpec);

            var questionsById = cardQuestions.ToDictionary(q => q.Id);

            foreach (var answer in dto.Answers)
            {
                if (!questionsById.TryGetValue(answer.QuestionId, out var question))
                {
                    context.AddFailure(
                        "Answers",
                        "أحد الأسئلة غير موجود ضمن أسئلة هذه البطاقة");

                    continue;
                }

                if (!IsAnswerValidForQuestionType(answer, question.Type))
                {
                    context.AddFailure(
                        "Answers",
                        "الإجابة غير مناسبة لنوع السؤال");
                }
            }
        }

        private static bool IsAnswerValidForQuestionType(
            AnswerRequestDto answer,
            QuestionType questionType)
        {
            switch (questionType)
            {
                case QuestionType.Boolean:
                    return answer.BooleanValue.HasValue;

                case QuestionType.Score:
                case QuestionType.FrequencyScore:
                    return answer.ScoreValue.HasValue;

                case QuestionType.Text:
                    return !string.IsNullOrWhiteSpace(answer.TextValue);

                case QuestionType.Options:
                    return !string.IsNullOrWhiteSpace(answer.SelectedOption);

                default:
                    return false;
            }
        }
    }


    public class AnswerRequestValidator : AbstractValidator<AnswerRequestDto>
    {
        public AnswerRequestValidator()
        {
            RuleFor(a => a.QuestionId)
                .NotEmpty()
                .WithMessage("معرف السؤال غير صالح");
        }
    }
}

