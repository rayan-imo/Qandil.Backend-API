using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.Children
{
    public class ChildAddResponse
    {
        public Guid Id { get; set; }
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

        public static ChildAddResponse Transform(Child child)
        {
            return new ChildAddResponse()
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
               Gender = child.Gender,
               DateOfBirth = child.DateOfBirth,
               PlaceOfBearth = child.PlaceOfBearth,
               Address = child.Address,
               JoiningDate = child.JoiningDate,
               IsEnrolledInSchool = child.IsEnrolledInSchool,
               SchoolName = child.SchoolName,
               SchoolGrade = child.SchoolGrade,
               HasDisability = child.HasDisability,
               MotherName = child.MotherName,
               FatherName = child.FatherName,
               MotherJob = child.MotherJob,
               FatherJob= child.FatherJob,
               FamilyMembers = child.FamilyMembers,
               
            };
        }
    }


}
