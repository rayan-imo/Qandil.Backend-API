using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.ChildTests
{
    public class ChildTestResponse
    {
        public Guid Id { get; set;  }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public TestType Type { get; set; }
        public float Result { get; set; }
        public bool IsPassed { get; set; }
        public int AttemptNumber { get; set; }
        public string? Nots { get; set; }
        public Guid EmployeeId { get; set; }
        public string TestName { get; set; }
        public static ChildTestResponse Transform(ChildTest childTest)
        {
            return new ChildTestResponse
            {
                Id = childTest.Id,
                Date = childTest.Date,
                Type = childTest.Type,
                Result = childTest.Result,
                Nots = childTest.Nots,
                EmployeeId = childTest.EmployeeId,
                FullName = $"{childTest.Child.FirstName} {childTest.Child.FatherName} {childTest.Child.LastName}",
                TestName = childTest.Test.Name,
            };
        }
    }
}
