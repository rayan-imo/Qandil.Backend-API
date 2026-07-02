using Microsoft.IdentityModel.Tokens;
using Qandil.Core.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Qandil.Core.Entity
{
    public class EvaluationCard : BaseEntity
    {
        public Guid DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public string CardName { get; set; }
        public string MainTitleScoresJson { get; set; }
     
    [NotMapped]
        public Dictionary<string, int> MainTitleScores
        {
            get => string.IsNullOrEmpty(MainTitleScoresJson)
                ? new Dictionary<string, int>()
                : JsonSerializer.Deserialize<Dictionary<string, int>>(MainTitleScoresJson);
            set => MainTitleScoresJson = JsonSerializer.Serialize(value);
        }

        public int TotalScore { get; set; }
        public string EvaluationMessage { get; set; }


    }
}
