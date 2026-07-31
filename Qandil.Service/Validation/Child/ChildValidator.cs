using FluentValidation;
using Qandil.Service.Dtos.ChildDto.Request;

namespace Qandil.Service.Validation.Child
{
    public class ChildValidator : AbstractValidator<ChildAddRequesDto>
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

            RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("تاريخ الميلاد مطلوب");



        }



    }
}