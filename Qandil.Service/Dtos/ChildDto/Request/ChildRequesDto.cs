<<<<<<< HEAD
=======

>>>>>>> d919681 (Add AuthServices)
using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.ChildDto.Request;

public class ChildRequesDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
<<<<<<< HEAD
    public Gender? Gender { get; set; }
    public DateTime JoiningDate { get; set; }
    public string MotherName { get; set; }
    public string FatherName { get; set; }

    public required DateTime DateOfBirth { get; set; }
  
    public required string Address { get; set; }
    public bool HasDisability { get; set; } = false;

}
=======
    public string MotherName { get; set; }
    public string FatherName { get; set; }
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
    public bool HasDisability { get; set; }

}
>>>>>>> d919681 (Add AuthServices)
