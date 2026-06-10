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
        public required DateTime DateOfBirth { get; set; }
      
        public required string Address { get; set; }
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
                Address = child.Address,
                JoiningDate = child.JoiningDate,
                HasDisability = child.HasDisability,

            };
        }
    }


}
