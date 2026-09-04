namespace Qandil.API.Dtos.Responses.DiagnosisAnswers
{
    public class CardAnswerDetailResponse
    {
        public Guid? AnswerId { get; set; }
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string SubTitle { get; set; }
        public int? ScoreValue { get; set; }
        public string DisplayAnswer { get; set; }
    }
}
