using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Diagnosises
{
    public class DiagnosisResponse
    {
        public Guid Id { get; set; }
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }

        public static DiagnosisResponse Transform(Diagnosis diagnosi)
        {
            return new DiagnosisResponse
            {
                Id=diagnosi.Id,
                DisabilityOnsetDate = diagnosi.DisabilityOnsetDate,
                MedicalNots = diagnosi.MedicalNots,
                EmployeeId = diagnosi.EmployeeId,
                ChildId = diagnosi.ChildId,
            };
        }
    

    }
}
