using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.Dtos.QuestionDto.Responses;
namespace Qandil.Service.Dtos.DiagnosisDto.Response
{
    public class FullDiagnosisResponseDto
    {
        public Guid DiagnosisId { get; set; }
        public Guid ChildId { get; set; }
        public List<QuestionGroupDto> DiagnosisQuestions { get; set; }
        public List<EvaluationCardDto> Evaluations { get; set; }
    }
}
