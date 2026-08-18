using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public  class Subject : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TestSubject> TestSubjects {  get; set; }
        public ICollection<ChildTestSubjectMark> ChildTestSubjectMarks { get; set; }
    }
}
