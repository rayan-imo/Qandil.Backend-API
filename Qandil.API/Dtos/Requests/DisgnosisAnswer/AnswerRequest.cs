namespace Qandil.API.Dtos.Requests.DisgnosisAnswer
{
    public class AnswerRequest
    {
        public Guid QuestionId { get; set; }
        public int? ScoreValue { get; set; }
        public bool? BooleanValue { get; set; }
        public string? TextValue { get; set; }
        public string? SelectedOption { get; set; }
        public string? Notes { get; set; }
    }
}
