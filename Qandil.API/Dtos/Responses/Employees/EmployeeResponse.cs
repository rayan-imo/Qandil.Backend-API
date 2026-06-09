using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Employees
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Specicality { get; set; }

        public static EmployeeResponse Transform(Employee employee)
        {
            return new EmployeeResponse()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Specicality = employee.Specicality

            };
        }
    }
}

