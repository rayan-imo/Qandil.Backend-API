using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public partial class Subject : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<TestSubject> TestSubjects { get; set; }
        public ICollection<ChildTestSubjectMark> ChildTestSubjects {  get; set; } 
      
    }
}
