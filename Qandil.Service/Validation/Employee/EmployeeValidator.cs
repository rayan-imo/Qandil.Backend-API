using FluentValidation;
using Qandil.Service.Dtos.Employee.Request;

namespace Qandil.Service.Validation.Employee
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequestDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("الاسم الأول مطلوب");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("الاسم الأخير مطلوب");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة");

        }

    }
}
