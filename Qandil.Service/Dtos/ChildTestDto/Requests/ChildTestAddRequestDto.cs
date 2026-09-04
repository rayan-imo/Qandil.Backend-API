using Qandil.Core.Enums;
using Qandil.Service.Dtos.SubjectMarkDto.Request;

namespace Qandil.Service.Dtos.ChildTestDto.Requests
{
    public class ChildTestAddRequestDto
    {
        public TestType Type { get; set; }
        public string? Nots { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }
        public List<SubjectMarkRequestDto> SubjectMarkDtos { get; set; }


    }
}
