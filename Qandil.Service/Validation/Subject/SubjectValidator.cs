using FluentValidation;
using Qandil.Service.Dtos.LevelDto.Request;
using Qandil.Service.Dtos.SubjectDto.Request;

namespace Qandil.Service.Validation.Level
{
    public class SubjectValidator : AbstractValidator<SubjectRequestDto>
    {
        public SubjectValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم المادة")
              .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على محارف فقط");
            


        }
    }
}
