namespace Qandil.API.Dtos.Requests.Diagnisis
{
    public class DiagnosisRequest
    {
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
        public Guid ChildId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
