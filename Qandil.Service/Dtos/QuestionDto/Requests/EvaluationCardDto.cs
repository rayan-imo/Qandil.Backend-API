namespace Qandil.Service.Dtos.QuestionDto.Requests
{
    public class EvaluationCardDto
    {
        public string CardName { get; set; }
        public Dictionary<string, int> MainTitleScores { get; set; }
        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }
    }
}
