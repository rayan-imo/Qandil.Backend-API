using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class Answer : BaseEntity
    {
        public Guid DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }

        // حسب نوع السؤال
        public bool? BooleanValue { get; set; } 
        
        // نعم/لا
        public int? ScoreValue { get; set; }          // 0,1,2,3
        public string? TextValue { get; set; }        // نص
        public string? SelectedOption { get; set; }   // للخيارات المتعدد 
        public string? Notes { get; set; }

    }


}
