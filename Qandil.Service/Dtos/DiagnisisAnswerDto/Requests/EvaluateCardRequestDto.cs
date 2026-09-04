using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.AnswerDto.Requests
{
    public class EvaluateCardRequestDto
    {

        public Guid DiagnosisId { get; set; }
        public CardType CardType { get; set; }
        public List<AnswerRequestDto> Answers { get; set; }
    }
}
