using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.DiagnosisAnswers
{
    public class CardDetailsResponse
    {
        public CardType CardType { get; set; }
        public string DisplayName { get; set; }
        public List<CardAnswerDetailResponse> Answers { get; set; } = new();
    }
}
