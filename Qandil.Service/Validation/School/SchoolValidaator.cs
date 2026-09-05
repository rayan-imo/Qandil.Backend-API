using FluentValidation;
using Qandil.Service.Dtos.SchoolDto.Request;

namespace Qandil.Service.Validation.School
{
    public class SchoolValidaator : AbstractValidator<SchoolRequestDto>
    {
        public SchoolValidaator()
        {
            RuleFor(x => x.SchoolName)
              .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم المدرسة")
                .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على محارف فقط");
            ;
        }
    }
}
