using FluentValidation;
using Qandil.Service.Dtos.DiagnosisDto.Requests;

namespace Qandil.Service.Validation.Diagnosis
{
    public class DiagnosisValidator : AbstractValidator<DiagnosisRequestDto>
    {
        public DiagnosisValidator()
        {
           

            RuleFor(x => x.DisabilityOnsetDate)
             .NotEmpty()
             .WithMessage(" تاريخ ظهور الإعاقة مطلوب ");


            RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .WithMessage(" معرف أخصائي التشخيص مطلوب ");


            RuleFor(x => x.ChildId)
              .NotEmpty()
             .WithMessage(" معرف الطفل مطلوب ");


        }
    }
}
