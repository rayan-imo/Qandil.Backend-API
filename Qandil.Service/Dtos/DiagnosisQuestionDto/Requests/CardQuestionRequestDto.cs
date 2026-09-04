using Qandil.Core.Enums;
using Qandil.Service.Dtos.QuestionOptionsDto.Requests;

namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Requests
{
    public class CardQuestionRequestDto
    {
        public CardType CardType { get; set; }
        public string SubTitle { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; } = QuestionType.Score;   // ثابت دايماً Score لأسئلة البطاقات

        public int? MinValue { get; set; }                             // مطلوب بس لو RawNumber
        public int? MaxValue { get; set; }                             // مطلوب بس لو RawNumber

        public int Order { get; set; }
    }
}
