using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.EvaluationResultDtos.Responses
{
    public class EvaluationResponseDto
    {

        public Guid DiagnosisId { get; set; }
      
        public string CardName { get; set; }
        public List<Dictionary<string, int>> MainTitleScores { get; set; }
        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }
    }
}
