using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.Diagnosis.Requests
{
    public class DiagnosisDto
    {
        public Guid DiagnosisId { get; set; }
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
        public string StatusDescription { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }

    }
}
