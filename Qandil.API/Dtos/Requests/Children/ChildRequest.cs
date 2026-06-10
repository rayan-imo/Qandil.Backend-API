using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.Children
{
    public class ChildRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Gender? Gender { get; set; }
        public required DateTime JoiningDate { get; set; }
        public required string MotherName { get; set; }
        public required string FatherName { get; set; }
<<<<<<< HEAD

        public required DateTime DateOfBirth { get; set; }
        public required string Address { get; set; }
=======
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        public required string GuardianName { get; set; }
        public required string GuardianPhoneNumber { get; set; }
        public string GuardianRelationship { get; set; }
        public EducationLevel FatherEducationLevel { get; set; }
        public EducationLevel MotherEducationLevel { get; set; }
        public int ChildOrderAmongSiblings { get; set; }
        public int TotalFamilyMembers { get; set; }
        public string Address { get; set; }
>>>>>>> d919681 (Add AuthServices)
        public bool HasDisability { get; set; }
    }
}
