using FluentValidation;
using Qandil.Service.Dtos.Program.Requests;

namespace Qandil.Service.Validation.Program
{
    public class ProgramValidator:AbstractValidator<EduProgramRequestDto>
    {
        public ProgramValidator()
        {
       
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("اسم البرنامج مطلوب ");

            RuleFor(x => x.SessionNumber)
            .NotEmpty().WithMessage("عدد الجلسات مطلوب ");

            RuleFor(x => x.SessionDuration)
            .NotEmpty().WithMessage("مدة الحصة الدرسية مطلوبة ");




        }
    }
}
