using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.EvaluationCardDtos.Requests
{
    public class EvaluationCardsDto
    {
        public Guid DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public string CardName { get; set; }
        public List<Dictionary<string, int>> MainTitleScores { get; set; }
        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }
    }
}
