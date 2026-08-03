using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.ChildTests
{
    public class ChildTestRequest
    {
        public DateTime Date { get; set; }
        public TestType Type { get; set; }
        public float Mark { get; set; }
        public string? Nots { get; set; }
        public string? Description { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }

    }
}
