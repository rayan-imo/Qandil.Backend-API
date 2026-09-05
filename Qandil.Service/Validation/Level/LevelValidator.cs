using FluentValidation;
using Qandil.Service.Dtos.LevelDto.Request;

namespace Qandil.Service.Validation.Level
{
    public class LevelValidator : AbstractValidator<LevelRequestDto>
    {
        public LevelValidator()
        {
            RuleFor(x => x.LevelName)
              .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم المستوى")
              .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على محارف فقط"); 

            RuleFor(x => x.ProgramName)
             .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم البرنامج")
             .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على محارف فقط"); 

        }
    }
}
