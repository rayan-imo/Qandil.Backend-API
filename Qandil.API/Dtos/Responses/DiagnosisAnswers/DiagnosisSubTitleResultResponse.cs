namespace Qandil.API.Dtos.Responses.DiagnosisAnswers
{
    public class DiagnosisSubTitleResultResponse
    {
        public string SubTitle { get; set; }
        public List<DiagnosisAnswerDetailResponse> Answers { get; set; } = new();
    }
}
