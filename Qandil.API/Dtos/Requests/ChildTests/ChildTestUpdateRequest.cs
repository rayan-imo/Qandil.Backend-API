using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.ChildTests
{
    public class ChildTestUpdateRequest
    {
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }
        public TestType Type { get; set; }
        public Guid EmployeeId { get; set; }
        public string? Nots { get; set; }

    }
}
