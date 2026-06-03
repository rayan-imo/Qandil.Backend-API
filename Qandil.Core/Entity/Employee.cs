using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email {  get; set; }
        public string? Specicality { get; set; }
        public ICollection<Diagnosis>? Diagnoses { get; set; }
        public ICollection<SupportivSession>? SupportivSessions { get; set; }
        public ICollection<Test>? Tests { get; set; }
        public ICollection<ReEvalution>? ReEvalutions { get; set; }
        public ICollection<Classroom>? Classrooms { get; set; }

    }
}
