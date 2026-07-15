using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Questions
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

        public static DiagnosisQuestionResponse Transform(Question question)
        {
            return new DiagnosisQuestionResponse
            {
                Id = question.Id,
               SubTitle=question.SubTitle,
                MainTitle = question.MainTitle,
                QuestionText = question.QuestionText,
                Type = question.Type.ToString(),  // تحويل Enum إلى نص
                Options = question.Options,
                Order = question.Order
            };
        }


    }
}
