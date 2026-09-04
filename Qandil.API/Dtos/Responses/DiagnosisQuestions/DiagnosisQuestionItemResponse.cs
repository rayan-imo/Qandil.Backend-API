using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class DiagnosisQuestionItemResponse
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public int Order { get; set; }
        public List<DiagnosisOptionResponse> Options { get; set; } = new();
    }
}
