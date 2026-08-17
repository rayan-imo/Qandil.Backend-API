using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class ChildTestSubjectMark:BaseEntity
    {
        public double ObtainMark {  get; set; }
<<<<<<< HEAD
=======
        public int AttemptNumber {  get; set; }
        public DateTime TakenAt { get; set; }
        public string Notes {  get; set; }
        public Guid TestSubjectId { get; set; }
        public TestSubject TestSubject { get; set; }
>>>>>>> 3fec87b (Add Repo)
        public Guid ChildTestId { get; set; }
        public ChildTest ChildTest { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
        public Guid EmployeeId {  get; set; }
        public Employee Employee { get; set; }
    }
}
