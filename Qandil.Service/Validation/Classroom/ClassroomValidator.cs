using FluentValidation;
using Qandil.Service.Dtos.ClassRoom.Requests;

namespace Qandil.Service.Validation.Classroom
{
    class ClassroomValidator : AbstractValidator<ClassroomRequestDto>
    {
        public ClassroomValidator()
        {
            RuleFor(x => x.MaxCapacity)
            .NotEmpty().WithMessage(" السعة العظمى للصف مطلوبة ");

            RuleFor(x => x.CurrentCapacity)
          .NotEmpty().WithMessage(" السعة الحالية للصف مطلوبة ");

            RuleFor(x => x.ProgramId)
          .NotEmpty().WithMessage("معرف البرنامج للصف مطلوب ");

            RuleFor(x => x.LevelId)
          .NotEmpty().WithMessage("معرف المستوى للصف مطلوب ");

            RuleFor(x => x.EmployeeId)
          .NotEmpty().WithMessage("معرف المعلم للصف مطلوب ");

        }
    }
}
