using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.QuestionDto.Responses
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string MainTitle { get; set; }
        public string? SubTitle { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }    // Boolean, Score, Text, Options
        public List<string>? Options { get; set; }  // للـ Options فقط
        public int Order { get; set; }

        public static QuestionResponse Transform(Question question)
        {
            return new QuestionResponse
            {
                Id = question.Id,
                MainTitle = question.MainTitle,
                SubTitle = question.SubTitle,
                QuestionText = question.QuestionText,
                Type = question.Type,
                Options = question.Options,
                Order = question.Order

            };
        }
    }
    
}
