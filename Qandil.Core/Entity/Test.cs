using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class Test:BaseEntity
    {
        public string Name { get; set; }
        public Level Level { get; set; }
        public Guid LevelId { get; set; }  
        public Subject Subject { get; set; }
        public Guid SubjectId {  get; set; }
        public ICollection<ChildTest> ChildTests { get; set; }
        public ICollection<TestSubject> TestSubjects { get; set; }

       
    }
}
