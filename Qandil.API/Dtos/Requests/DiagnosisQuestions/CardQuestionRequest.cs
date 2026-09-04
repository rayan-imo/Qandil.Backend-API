using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.DiagnosisQuestions
{
    public class CardQuestionRequest
    {
        public CardType CardType { get; set; }
        public string SubTitle { get; set; }
        public string QuestionText { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int Order { get; set; }
    }
}
