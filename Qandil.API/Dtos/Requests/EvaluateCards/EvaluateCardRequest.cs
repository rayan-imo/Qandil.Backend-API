using Qandil.Service.Dtos.AnswerDto.Requests;

namespace Qandil.API.Dtos.Requests.EvaluateCards
{
    public class EvaluateCardRequest
    {

        public Guid DiagnosisId { get; set; }
        public string CardName { get; set; }
        public List<AnswerRequestDto> Answers { get; set; }
    }
}
