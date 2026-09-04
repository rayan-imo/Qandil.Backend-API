using FluentValidation;
using Qandil.Service.Dtos.QuestionOptionsDto.Requests;

namespace Qandil.Service.Validation.QuestionOption
{
     public class QuestionOptionValidator : AbstractValidator<QuestionOptionRequestDto>
    {
        public QuestionOptionValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("نص الخيار مطلوب")
                .MaximumLength(100).WithMessage("نص الخيار لا يتجاوز 100 حرف");

            RuleFor(x => x.Value)
                .NotNull().WithMessage("قيمة الخيار مطلوبة")
                .GreaterThanOrEqualTo(0).WithMessage("قيمة الخيار يجب أن تكون 0 أو أكثر");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("الترتيب يجب أن يكون 0 أو أكثر");

            RuleFor(x => x.DiagnosisQuestionId)
                .NotEmpty().WithMessage("معرف السؤال مطلوب");
        }
    }
}
