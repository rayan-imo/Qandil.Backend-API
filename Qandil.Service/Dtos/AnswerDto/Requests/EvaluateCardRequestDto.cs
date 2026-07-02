namespace Qandil.Service.Dtos.AnswerDto.Requests
{

    public class EvaluateCardRequestDto
    {

        public Guid DiagnosisId { get; set; }
        public string CardName { get; set; }
        public List<AnswerRequestDto> Answers { get; set; }
    }
}
