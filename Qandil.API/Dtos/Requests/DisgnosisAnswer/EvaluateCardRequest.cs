using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.DisgnosisAnswer
{
    public class EvaluateCardRequest
    {
        public Guid DiagnosisId { get; set; }
        public CardType CardType { get; set; }
        public List<AnswerRequest> Answers { get; set; }
    }
}
