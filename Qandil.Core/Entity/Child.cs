using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class Child : BaseEntity
    {
        // معلومات عامة 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime JoiningDate {  get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
<<<<<<< HEAD
       

        public DateTime DateOfBirth { get; set; }
=======
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        public string GuardianName { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public string GuardianRelationship { get; set; }
        public EducationLevel FatherEducationLevel { get; set; }
        public EducationLevel MotherEducationLevel { get; set; }
        public int ChildOrderAmongSiblings { get; set; }
        public int TotalFamilyMembers { get; set; }
>>>>>>> d919681 (Add AuthServices)
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
