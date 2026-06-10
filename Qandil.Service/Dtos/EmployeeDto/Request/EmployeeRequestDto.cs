namespace Qandil.Service.Dtos.Employee.Request
{
    public class EmployeeRequestDto
    {
       
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public int Age { get; set; }
        public string? Specicality { get; set; }
    }
}
  