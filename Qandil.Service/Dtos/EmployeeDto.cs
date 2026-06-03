namespace Qandil.Service.Dtos
{
    public class EmployeeDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public int Age { get; set; }
        public string? Specicality { get; set; }
    }
}
  