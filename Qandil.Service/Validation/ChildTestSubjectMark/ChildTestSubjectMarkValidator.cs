using FluentValidation;
using Qandil.Service.Dtos.ChildTestDto.Requests;
using Qandil.Service.Dtos.ChildTestSubjectMarkDto.Request;

namespace Qandil.Service.Validation.ChildTestSubjectMark
{
    public class ChildTestSubjectMarkValidator:AbstractValidator<ChildTestSubjectMarkRequestDto>
    {
        public ChildTestSubjectMarkValidator()
        {
            RuleFor(x => x.ChildTestId)
             .NotEmpty().WithMessage("معرف الاختبار للطفل مطلوب");

            RuleFor(x => x.SubjectId)
           .NotEmpty().WithMessage("معرف المادة مطلوب");

            RuleFor(x => x.EmployeeId)
           .NotEmpty().WithMessage("معرف الموظف مطلوب");
        }

    }
       
}
