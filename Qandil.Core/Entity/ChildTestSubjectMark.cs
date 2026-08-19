using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class ChildTestSubjectMark:BaseEntity
    {
        public double ObtainMark {  get; set; }
        public string? Notes {  get; set; }
        public Guid ChildTestId { get; set; }
        public ChildTest ChildTest { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Guid EmployeeId {  get; set; }
        public Employee Employee { get; set; }
    }
}
