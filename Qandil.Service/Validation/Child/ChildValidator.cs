using FluentValidation;
using Qandil.Service.Dtos;

namespace Qandil.Service.Validation.Child
{
    public class ChildValidator : AbstractValidator<ChildDto>
    {
        public ChildValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("الاسم الأول مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("الاسم الأخير مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.MotherName)
            .NotEmpty().WithMessage("اسم الأم مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("اسم الأب مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.GuardianName)
            .NotEmpty().WithMessage("اسم ولي ألأمر مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.GuardianPhoneNumber)
            .NotEmpty().WithMessage("رقم الهاتف مطلوب")
            .Must(x => x.All(char.IsDigit)).WithMessage("رقم الهاتف يجب أن يحتوي على أرقام فقط")
            .Length(10).WithMessage("رقم الهاتف يجب أن يحتوي 10 أرقام");

            RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("تاريخ الميلاد مطلوب");



        }



    }
}