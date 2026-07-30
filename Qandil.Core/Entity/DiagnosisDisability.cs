using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class DiagnosisDisability:BaseEntity
    {

        public Guid DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }
        public Guid DisabilityId { get; set; }
        public Disability Disability { get; set; }

    } 
}
