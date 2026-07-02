using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.Dtos.QuestionDto.Responses;

namespace Qandil.API.Dtos.Responses.Diagnosises
{
    public class FullDiagnosisResponse
    {
        public Guid DiagnosisId { get; set; }
        public Guid ChildId { get; set; }
        public List<QuestionGroupDto> DiagnosisQuestions { get; set; }
        public List<EvaluationCardDto> Evaluations { get; set; }
    }
}
