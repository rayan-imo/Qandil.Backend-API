using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class DiagnosisQuestionResponse
    {
        public string SubTitle { get; set; }
        public List<DiagnosisQuestionItemResponse> Questions { get; set; } = new();
    }
}
