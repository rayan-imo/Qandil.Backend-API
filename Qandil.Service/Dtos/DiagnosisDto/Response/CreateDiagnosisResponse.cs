using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.DiagnosisDto.Response
{
    public class CreateDiagnosisResponse
    {
        public DateTime DisabilityOnsetDate { get; set; }

        public string? MedicalNots { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid ChildId { get; set; }

        public static CreateDiagnosisResponse Transform(Diagnosis diagnosis)
        {
            return new CreateDiagnosisResponse
            {
                DisabilityOnsetDate = diagnosis.DisabilityOnsetDate,
                MedicalNots = diagnosis.MedicalNots,
                EmployeeId = diagnosis.EmployeeId,
                ChildId = diagnosis.ChildId,
            };
        }
    }
}
