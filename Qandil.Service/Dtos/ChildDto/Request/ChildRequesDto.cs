<<<<<<<< HEAD:Qandil.Service/Dtos/ChildDto/ChildDto.cs
﻿namespace Qandil.Service.Dtos.ChildDto
========
﻿namespace Qandil.Service.Dtos.ChildDto.Request
>>>>>>>> 16611d2ed4bdcb19996281e0e4b2b1998ff2548b:Qandil.Service/Dtos/ChildDto/Request/ChildRequesDto.cs
{
    public class ChildRequesDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string MotherName { get; set; }
        public required string FatherName { get; set; }
        public  string? Gender { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required string GuardianName { get; set; }
        public required string GuardianPhoneNumber { get; set; }
        public required string GuardianRelationship { get; set; }
        public required string Address { get; set; }
        public bool HasDisability { get; set; }=false;

    }
}
