using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class CardQuestionResponse
    {
        public Guid Id { get; set; }
        public string CardName { get; set; }
        public string? SubTitle { get; set; }
        public string QuestionText { get; set; }
        public string Type { get; set; }    // Boolean, Score, Text, Options
        public List<string>? Options { get; set; }  // للـ Options فقط
        public int Order { get; set; }

        public static CardQuestionResponse Transform(DiagnosisQuestion question)
        {
            return new CardQuestionResponse
            {
                Id = question.Id,
                CardName = question.CardName,
                SubTitle = question.SubTitle,
                QuestionText = question.QuestionText,
                Type = question.Type.ToString(),  // تحويل Enum إلى نص
                Options = question.Options,
                Order = question.Order
            };
        }


    }
}
