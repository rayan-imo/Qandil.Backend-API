namespace Qandil.API.Dtos.Requests.DisgnosisAnswer
{
    public class SaveDiagnosisSubTitleAnswersRequest
    {
        public Guid DiagnosisId { get; set; }
        public string SubTitle { get; set; }
        public List<AnswerRequest> Answers { get; set; }
    }
}
