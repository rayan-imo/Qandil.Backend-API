using Microsoft.AspNetCore.Authentication.Cookies;
using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
   
        public class TestSubject:BaseEntity
        {
            public Guid TestId{ get; set; }
            public Test Test { get; set; }
            public Guid SubjectId { get; set; }
            public Subject Subject { get; set; }
<<<<<<< HEAD
=======
            public double MaxMark {  get; set; }
           public ICollection<ChildTestSubjectMark> ChildTestSubjectMarks { get; set; }  
>>>>>>> 3fec87b (Add Repo)

        }
}
