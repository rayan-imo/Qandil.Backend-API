using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.Validation.DiagnosisQuestions
{
    public class CardQuestionAddValidator : AbstractValidator<CardQuestionRequestDto>
    {
        public CardQuestionAddValidator()
        {

            RuleFor(x => x.CardType)
                .IsInEnum().WithMessage("نوع البطاقة غير صالح")
                .Must(ct => ct != CardType.None).WithMessage("يرجى اختيار نوع البطاقة");

            RuleFor(x => x.SubTitle)
                .NotEmpty().WithMessage("العنوان الجانبي مطلوب")
                .MaximumLength(200).WithMessage("العنوان الجانبي لا يتجاوز 200 حرف");

            RuleFor(x => x.QuestionText)
                .NotEmpty().WithMessage("نص السؤال مطلوب")
                .MaximumLength(500).WithMessage("نص السؤال لا يتجاوز 500 حرف");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("الترتيب يجب أن يكون 0 أو أكثر");

            // ===== لو البطاقة من نوع RawNumber (كل البطاقات ما عدا ولي الأمر) =====
            When(x => CardScoreInputTypeResolver.Resolve(x.CardType) == ScoreInputType.RawNumber, () =>
            {
                RuleFor(x => x.MinValue)
                    .NotNull().WithMessage("الحد الأدنى مطلوب لهذا النوع من البطاقات");

                RuleFor(x => x.MaxValue)
                    .NotNull().WithMessage("الحد الأعلى مطلوب لهذا النوع من البطاقات")
                    .GreaterThan(x => x.MinValue)
                    .When(x => x.MinValue.HasValue)
                    .WithMessage("الحد الأعلى يجب أن يكون أكبر من الحد الأدنى");
            });

            // ===== بطاقة ولي الأمر (Frequency) ما بتحتاج Min/Max =====
            When(x => CardScoreInputTypeResolver.Resolve(x.CardType) == ScoreInputType.Frequency, () =>
            {
                RuleFor(x => x.MinValue)
                    .Must(v => v == null).WithMessage("لا حاجة لتحديد حد أدنى لهذه البطاقة");

                RuleFor(x => x.MaxValue)
                    .Must(v => v == null).WithMessage("لا حاجة لتحديد حد أعلى لهذه البطاقة");
            });
        }
    }
}
