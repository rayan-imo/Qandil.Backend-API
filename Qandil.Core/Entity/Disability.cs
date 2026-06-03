using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class Disability : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<DiagnosisDisability> DiagnosisDisabilities { get; set; }
    }
}
