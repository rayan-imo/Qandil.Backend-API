namespace Qandil.Service.Dtos.DiagnosisDto.Requests
{
    public class CreateDiagnosisRequest
    {
        public DateTime DisabilityOnsetDate { get; set; }

        public string? MedicalNots { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid ChildId { get; set; }
    }
}
