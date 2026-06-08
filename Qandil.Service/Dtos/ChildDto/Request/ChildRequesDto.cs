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

