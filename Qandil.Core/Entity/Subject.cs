using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public  class Subject : BaseEntity
    {
        public string Name { get; set; }
<<<<<<< HEAD
        public ICollection<TestSubject> TestSubjects { get; set; }
        public ICollection<ChildTestSubjectMark> ChildTestSubjects {  get; set; } 
      
=======
        public ICollection<TestSubject> TestSubjects {  get; set; }
        public ICollection<ChildTestSubjectMark> ChildTestSubjectMarks { get; set; }


>>>>>>> 3fec87b (Add Repo)
    }
}
