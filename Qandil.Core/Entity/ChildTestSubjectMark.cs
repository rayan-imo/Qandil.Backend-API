namespace Qandil.Core.Entity
{
    public class ChildTestSubjectMark
    {
        public double ObtainMark {  get; set; }
        public Guid ChildTestId { get; set; }
        public ChildTest ChildTest { get; set; }
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
