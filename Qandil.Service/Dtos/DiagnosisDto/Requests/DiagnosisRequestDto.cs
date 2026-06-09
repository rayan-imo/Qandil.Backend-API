using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.Diagnosis.Requests
{
    public class DiagnosisRequestDto
    {
<<<<<<< HEAD:Qandil.Service/Dtos/DiagnosisDto/Requests/DiagnosisRequestDto.cs
=======
        public Guid DiagnosisId {  get; set; }
>>>>>>> ee749da (AddEmployeeControllerAndEditResponseMessage):Qandil.Service/Dtos/Diagnosis/Requests/DiagnosisDto.cs
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
        public string StatusDescription { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }

    }
}
