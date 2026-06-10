using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.Children
{
    public class ChildResponse
    {  
        public Guid Id { get; set; }
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
        public string GuardianName { get; set; }
        public string GuardianPhoneNumber { get; set; }
        public string GuardianRelationship { get; set; }
        public EducationLevel FatherEducationLevel { get; set; }
        public EducationLevel MotherEducationLevel { get; set; }
        public int ChildOrderAmongSiblings { get; set; }
        public int TotalFamilyMembers { get; set; }
        public string Address { get; set; }
>>>>>>> d919681 (Add AuthServices)
        public bool HasDisability { get; set; }

        public static ChildResponse Transform(Child child)
        {
            return new ChildResponse()
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                MotherName = child.MotherName,
                FatherName = child.FatherName,
                Gender = child.Gender,
                DateOfBirth = child.DateOfBirth,
<<<<<<< HEAD
                Address = child.Address,
                JoiningDate = child.JoiningDate,
                HasDisability = child.HasDisability,

=======
                BirthPlace= child.BirthPlace,
                GuardianName = child.GuardianName,
                GuardianPhoneNumber = child.GuardianPhoneNumber,
                GuardianRelationship = child.GuardianRelationship,
                FatherEducationLevel= child.FatherEducationLevel,
                MotherEducationLevel = child.MotherEducationLevel,
                ChildOrderAmongSiblings= child.ChildOrderAmongSiblings,
                TotalFamilyMembers= child.TotalFamilyMembers,
                Address = child.Address,
                HasDisability = child.HasDisability
>>>>>>> d919681 (Add AuthServices)
            };
        }
    }


}
