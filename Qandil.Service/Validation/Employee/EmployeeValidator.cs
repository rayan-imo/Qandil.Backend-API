using FluentValidation;
using Qandil.Service.Dtos.EmployeeDto.Request;

namespace Qandil.Service.Validation.Employee
{
    public class EmployeeValidator : AbstractValidator<EmployeeRequestDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("الاسم الأول مطلوب")
            .Matches(@"^[\p{L}\s]+$") .WithMessage("الاسم يجب أن يحتوي على محارف فقط");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("الاسم الأخير مطلوب")
                  .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على محارف فقط"); ; ;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة");

            RuleFor(x => x.Age)
                 .GreaterThan(17)
                 .WithMessage("العمر يجب أن يكون 18 سنة أو أكثر");
        }

    }
}
