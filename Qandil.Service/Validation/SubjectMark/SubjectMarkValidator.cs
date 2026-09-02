using FluentValidation;
using Qandil.Service.Dtos.SubjectMarkDto.Request;

namespace Qandil.Service.Validation.SubjectMark
{
    public class SubjectMarkValidator : AbstractValidator<SubjectMarkRequestDto>
    {
        public SubjectMarkValidator()
        {
            RuleFor(x => x.SubjectId)
           .NotEmpty().WithMessage("معرف المادة مطلوب");


        }

    }

}
