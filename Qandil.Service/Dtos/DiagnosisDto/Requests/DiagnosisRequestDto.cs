using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.DiagnosisDto.Requests
{
    public class DiagnosisRequestDto
    {
        public DateTime DisabilityOnsetDate { get; set; }
        public string? MedicalNots { get; set; }
        public Guid EmployeeId { get; set; }
         public Guid ChildId { get; set; }
    }
}
