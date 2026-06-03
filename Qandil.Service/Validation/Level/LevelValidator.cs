using FluentValidation;
using Qandil.Service.Dtos;

namespace Qandil.Service.Validation.Level
{
    public class LevelValidator : AbstractValidator<LevelDto>
    {
        public LevelValidator()
        {
            RuleFor(x => x.LevelName)
              .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم المستوى");

            RuleFor(x => x.ProgramName)
             .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم البرنامج");

        }
    }
}
