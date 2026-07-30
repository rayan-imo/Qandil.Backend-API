using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class DiagnosisQuestionResponse
    {
        public Guid Id { get; set; }
        public string MainTitle { get; set; }
        public string? SubTitle { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }    // Boolean, Score, Text, Options
        public List<string>? Options { get; set; }  // للـ Options فقط
        public int Order { get; set; }

        public static DiagnosisQuestionResponse Transform(DiagnosisQuestion question)
        {
            return new DiagnosisQuestionResponse
            {
                Id = question.Id,
                MainTitle = question.MainTitle,
                SubTitle = question.SubTitle,
                QuestionText = question.QuestionText,
                Type = question.Type.ToString(),  // تحويل Enum إلى نص
                Options = question.Options,
                Order = question.Order
            };
        }


    }
}
