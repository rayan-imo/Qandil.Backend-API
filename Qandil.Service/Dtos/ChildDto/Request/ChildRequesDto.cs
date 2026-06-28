using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.ChildDto.Request;

public class ChildRequesDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateTime JoiningDate { get; set; }
    public string MotherName { get; set; }
    public string FatherName { get; set; }

    public required DateTime DateOfBirth { get; set; }
  
    public required string Address { get; set; }
    public bool HasDisability { get; set; } = false;

}

  

