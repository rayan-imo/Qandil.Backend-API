using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.ChildTestDto.Requests
{
    public class ChildTestRequestDto
    {
        public DateTime Date { get; set; }
        public TestType Type { get; set; }
        public float Mark { get; set; }
        public string Nots { get; set; }
        public Guid  EmployeeId { get; set; }
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }

    }
}
