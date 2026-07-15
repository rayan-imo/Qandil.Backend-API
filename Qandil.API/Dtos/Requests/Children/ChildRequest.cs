using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.Children
{
    public class ChildRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required Gender Gender { get; set; }
        public required DateTime JoiningDate { get; set; }
        public required string MotherName { get; set; }
        public required string FatherName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string Address { get; set; }
        public bool HasDisability { get; set; } 
    }
}
