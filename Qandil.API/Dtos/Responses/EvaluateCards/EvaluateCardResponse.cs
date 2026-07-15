using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.EvaluateCards
{
    public class EvaluateCardResponse
    {
        public Guid id { get; set; }
        public Guid DiagnosisId { get; set; }
        public string CardName { get; set; }
        public string MainTitleScoresJson { get; set; }
        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }


        public static EvaluateCardResponse Transform(EvaluationCard evaluationCard)
        {
            return new EvaluateCardResponse
            {
                id = evaluationCard.Id,
                DiagnosisId = evaluationCard.DiagnosisId,
                CardName = evaluationCard.CardName,
                MainTitleScoresJson = evaluationCard.MainTitleScoresJson,
                TotalScore = evaluationCard.TotalScore,
                EvaluationMessage = evaluationCard.EvaluationMessage,

            };

        }
    }
}
