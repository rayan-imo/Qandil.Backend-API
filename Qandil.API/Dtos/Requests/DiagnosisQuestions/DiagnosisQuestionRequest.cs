using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.DiagnosisQuestions
{
    public class DiagnosisQuestionRequest
    {
        public string? CardName { get; set; }
        public string MainTitle { get; set; }
        public string? SubTitle { get; set; }

        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }  // Boolean, Score, Text, Options
        public List<string>? Options { get; set; }  // للـ Options فقط
        public int Order { get; set; }
    }
}
