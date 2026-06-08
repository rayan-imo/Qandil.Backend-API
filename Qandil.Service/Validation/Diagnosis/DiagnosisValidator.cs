using FluentValidation;
using Qandil.Service.Dtos.Diagnosis.Requests;

namespace Qandil.Service.Validation.Diagnosis
{
    public class DiagnosisValidator : AbstractValidator<DiagnosisRequestDto>
    {
        public DiagnosisValidator()
        {
            RuleFor(x => x.DiagnosisId)
              .NotEmpty()
              .WithMessage(" معرف التشخيص مطلوب ");

            RuleFor(x => x.DisabilityOnsetDate)
             .NotEmpty()
             .WithMessage(" تاريخ ظهور الإعاقة مطلوب ");

            RuleFor(x => x.StatusDescription)
            .NotEmpty()
            .WithMessage(" وصف الحالة مطلوب ");


            RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .WithMessage(" معرف أخصائي التشخيص مطلوب ");


            RuleFor(x => x.EmployeeId)
              .NotEmpty()
             .WithMessage(" معرف الطفل مطلوب ");


        }
    }
}
