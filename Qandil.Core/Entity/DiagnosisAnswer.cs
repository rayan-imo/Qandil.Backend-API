using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class DiagnosisAnswer : BaseEntity
    {
        public bool? BooleanValue { get; set; } 
        public int? ScoreValue { get; set; } 
        public string? TextValue { get; set; }      
        public string? SelectedOption { get; set; }  
        public string? Notes { get; set; }

        public Guid DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public Guid QuestionId { get; set; }
        public DiagnosisQuestion Question { get; set; }

    }


}
