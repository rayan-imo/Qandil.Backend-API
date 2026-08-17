using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class Test:BaseEntity
    {
<<<<<<< HEAD
        public string Name { get; set; }
=======
        public string Title {  get; set; }
        public TestType TestType { get; set; }
        public bool HasPreTest {  get; set; }
>>>>>>> 3fec87b (Add Repo)
        public Level Level { get; set; }
        public Guid LevelId { get; set; }  
        public ICollection<ChildTest> ChildTests { get; set; }
        public ICollection<TestSubject> TestSubjects { get; set; }
<<<<<<< HEAD

=======
>>>>>>> 3fec87b (Add Repo)
       
    }
}
