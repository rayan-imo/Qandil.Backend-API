using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.AnswerDto.Requests
{
    public class AnswerRequestDto
    {
        public Guid QuestionId { get; set; }

        // لأسئلة البطاقات (Type == Score) — القيمة حسب ScoreInputType تبع السؤال
        public int? ScoreValue { get; set; }

        // لأسئلة التشخيص (Type == Boolean)
        public bool? BooleanValue { get; set; }

        // لأسئلة التشخيص (Type == Text)
        public string? TextValue { get; set; }

        // لأسئلة التشخيص (Type == Options)
        public string? SelectedOption { get; set; }
        public AnswerFrequency ? answerFrequency { get; set; }

        public string? Notes { get; set; }
    }


}
