using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class Diagnosis:BaseEntity
    {
        public DateTime DisabilityOnsetDate { get; set; }
        public string MedicalNots { get; set; }
         public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid ChildId { get; set; }
        public Child Child { get; set; }

        public ICollection<DiagnosisDisability> DiagnosisDisabilities { get; set; }
        public ICollection<EvaluationCard> EvaluationCards { get; set; }
        public ICollection<DiagnosisAnswer> DiagnosisAnswers { get; set; }






    }

}
