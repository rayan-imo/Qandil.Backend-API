using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class ChildTest:BaseEntity
    {
        public Child Child { get; set; }
        public Guid ChildId { get; set; }
        public Test Test { get; set; }
        public Guid TestId { get; set; }


    }
}
