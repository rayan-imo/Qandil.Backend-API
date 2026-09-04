using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.DiagnosisAnswers
{
    public class CardResultResponse
    {
        public CardType CardType { get; set; }
        public string DisplayName { get; set; }
        public Dictionary<string, int> SubTitleScores { get; set; } = new();
        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }
    }
}
