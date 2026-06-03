using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Children
{
    public class ChildResponse
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MotherName { get; set; }
        public required string FatherName { get; set; }
        public string? Gender { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string GuardianName { get; set; }
        public required string GuardianPhoneNumber { get; set; }
        public required string GuardianRelationship { get; set; }
        public required string Address { get; set; }
        public bool HasDisability { get; set; }

        public static ChildResponse Transform(Child child)
        {
            return new ChildResponse()
            {
                FirstName = child.FirstName,
                LastName = child.LastName,
                MotherName = child.MotherName,
                FatherName = child.FatherName,
                Gender = child.Gender,
                DateOfBirth = child.DateOfBirth,
                GuardianName = child.GuardianName,
                GuardianPhoneNumber = child.GuardianPhoneNumber,
                GuardianRelationship = child.GuardianRelationship,
                Address = child.Address,
                HasDisability = child.HasDisability,

            };
        }
    }


}
