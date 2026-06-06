using FluentValidation;
using Qandil.Service.Dtos.Disability.Requests;

namespace Qandil.Service.Validation.Disability
{
    public class DisabilityValidator: AbstractValidator<DisabilityDto>
    {
        public DisabilityValidator()
        {

            RuleFor(x => x.DisabilityId)
             .NotEmpty()
             .WithMessage(" معرف الإعاقة مطلوب ");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("أسم الإعاقة مطلوب")
            .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");
            
        }
    }
}
