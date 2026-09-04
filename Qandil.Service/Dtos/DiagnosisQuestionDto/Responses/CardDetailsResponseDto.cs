using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Responses
{
    public class CardDetailsResponseDto
    {
        public CardType CardType { get; set; }
        public string DisplayName { get; set; }
        public List<CardAnswerDetailDto> Answers { get; set; } = new();
    }

}
