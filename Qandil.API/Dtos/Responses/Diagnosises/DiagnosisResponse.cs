using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Diagnosises
{
    public class DiagnosisResponse
    {
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
        public string StatusDescription { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }

        public static DiagnosisResponse Transform(Diagnosis diagnosi)
        {
            return new DiagnosisResponse
            {
                DisabilityOnsetDate = diagnosi.DisabilityOnsetDate,
                MedicalNots = diagnosi.MedicalNots,
                StatusDescription = diagnosi.StatusDescription,
                EmployeeId = diagnosi.EmployeeId,
                ChildId = diagnosi.ChildId,
            };
        }
    

    }
}
