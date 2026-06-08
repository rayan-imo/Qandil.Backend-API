using FluentValidation;
using Qandil.Service.Dtos.Disability.Requests;

namespace Qandil.Service.Validation.Disability
{
    public class DisabilityValidator : AbstractValidator<DisabilityRequestDto>
    {
        public DisabilityValidator()
        {

          
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("أسم الإعاقة مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

        }
    }
}
