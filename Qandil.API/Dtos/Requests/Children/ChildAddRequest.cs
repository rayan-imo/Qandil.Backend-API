using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.Children
{
    public class ChildAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBearth { get; set; }
        public string Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsEnrolledInSchool { get; set; }
        public string? SchoolName { get; set; }
        public string? SchoolGrade { get; set; }
        public bool HasDisability { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string? FatherJob { get; set; }
        public string? MotherJob { get; set; }
        public int? FamilyMembers { get; set; }

       
    }
}
