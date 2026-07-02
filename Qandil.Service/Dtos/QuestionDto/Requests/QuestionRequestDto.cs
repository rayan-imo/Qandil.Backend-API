using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.QuestionDto.Requests
{
    public class QuestionRequestDto
    {
        public string MainTitle { get; set; }
        public string? SubTitle { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }  // Boolean, Score, Text, Options
        public string? OptionsJson { get; set; }  // للـ Options فقط
        public int Order { get; set; }
    }
}
