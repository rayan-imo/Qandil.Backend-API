using FluentValidation;
using Qandil.Service.Dtos.ChildTestDto.Requests;

namespace Qandil.Service.Validation.ChildTest
{
    public class ChildTestValidator : AbstractValidator<ChildTestRequestDto>
    {
        public ChildTestValidator()
        {
            RuleFor(x => x.Date)
                 .NotEmpty().WithMessage("تاريخ الاختبار مطلوب")
                 .LessThanOrEqualTo(DateTime.Now).WithMessage("تاريخ الاختبار لا يمكن أن يكون في المستقبل");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("نوع الاختبار مطلوب")
                .IsInEnum().WithMessage("نوع الاختبار غير صحيح");

            RuleFor(x => x.Mark)
                .NotNull().WithMessage("العلامة مطلوبة")
                .GreaterThanOrEqualTo(0).WithMessage("العلامة يجب ألا تكون سالبة");

            RuleFor(x => x.Nots)
                .NotEmpty().WithMessage("الملاحظات مطلوبة")
                .MaximumLength(500).WithMessage("الملاحظات لا تتجاوز 500 حرف");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("معرف الموظف مطلوب");

            RuleFor(x => x.ChildId)
                .NotEmpty().WithMessage("معرف الطفل مطلوب");

            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("معرف الاختبار مطلوب");
        }
    }
}
