using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.DiagnosisQuestions
{
    public class DiagnosisQuestionRequest
    {
        public string SubTitle { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public int Order { get; set; }
        public List<DiagnosisQuestionOptionRequest>? Options { get; set; }
    }
}
