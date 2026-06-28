using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class Child : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime JoiningDate {  get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool HasDisability { get; set; }
        public Diagnosis? Diagnosis { get; set; }
        public Guid? ProgramId { get; set; }
        public EduProgram? Program { get; set; }
        public Guid? ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }

        public Tracking? Tracking { get; set; }
        public ICollection<ChildTest> ChildTests { get; set; }
        public ICollection<SupportivSession>? SupportivSessions { get; set; }



    }
}
