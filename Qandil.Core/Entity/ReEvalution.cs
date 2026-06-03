using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class ReEvalution:BaseEntity
    {
        public Employee Employee { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

    }
}
