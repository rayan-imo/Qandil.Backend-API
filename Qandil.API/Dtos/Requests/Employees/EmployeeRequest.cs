using System.ComponentModel.DataAnnotations;

namespace Qandil.API.Dtos.Requests.Employees
{
    public class EmployeeRequest
    {
        public required string FirstName { get; set; }
        public  required string LastName { get; set; }
        public  int Age { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public string? Specicality { get; set; }
    }
}
