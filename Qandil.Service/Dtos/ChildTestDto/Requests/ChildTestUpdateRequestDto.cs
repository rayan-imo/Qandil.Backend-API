using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.ChildTestDto.Requests
{
    public class ChildTestUpdateRequestDto
    {
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }
        public TestType Type { get; set; }
        public Guid EmployeeId { get; set; }
        public string? Nots { get; set; }

    }
}
