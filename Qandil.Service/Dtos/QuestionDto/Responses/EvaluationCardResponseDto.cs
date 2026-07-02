namespace Qandil.Service.Dtos.QuestionDto.Responses
{
    public class EvaluationCardResponseDto
    {
        public string CardName { get; set; }
        public List<Dictionary<string, int>> MainTitleScores { get; set; }
        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }
    }
    
}
