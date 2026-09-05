using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Diagnosises
{
    public class DiagnosisResponse
    {
        public Guid Id { get; set; }
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
        public Guid ChildId { get; set; }
        public Guid EmployeeId { get; set; }

        public static DiagnosisResponse Transform(Diagnosis diagnosis)
        {
            return new DiagnosisResponse
            {
                Id = diagnosis.Id,
                DisabilityOnsetDate = diagnosis.DisabilityOnsetDate,
                MedicalNots = diagnosis.MedicalNots,
                ChildId = diagnosis.ChildId,
                EmployeeId = diagnosis.EmployeeId
            };
        }
    }
}
