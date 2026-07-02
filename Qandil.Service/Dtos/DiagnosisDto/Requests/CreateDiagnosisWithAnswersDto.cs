using Qandil.Service.Dtos.AnswerDto.Requests;

namespace Qandil.Service.Dtos.DiagnosisDto.Requests
{
    public class CreateDiagnosisWithAnswersDto
    {
        public DateTime DisabilityOnsetDate { get; set; }

        public string? MedicalNots { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid ChildId { get; set; }

        public List<AnswerRequestDto> Answers { get; set; }
    }
}
