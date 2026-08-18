using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class ChildTest : BaseEntity
    {   
        public DateTime Date { get; set; }
        public TestType Type { get; set; }
        public float Result { get; set; }
        public bool IsPassed { get; set; }
        public int AttemptNumber { get; set; }
        public string? Nots { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid ChildId { get; set; }
        public Child Child { get; set; }
        public Test Test { get; set; }
        public Guid TestId { get; set; }
        public ICollection<ChildTestSubjectMark> ChildTestSubjectMarks { get; set; }

    }
}
