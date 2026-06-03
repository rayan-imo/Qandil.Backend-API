using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Employees
{
    public class EmployeeResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Specicality { get; set; }

        public static EmployeeResponse Transform(Employee employee)
        {
            return new EmployeeResponse()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Specicality = employee.Specicality

            };
        }
    }
}

