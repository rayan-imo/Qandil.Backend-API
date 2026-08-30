

namespace Qandil.Service.Dtos.ChildTestDto.Responses
{
    public class TestWithMarksResponseDto
    {
        public Guid ChildTestId { get; set; }
        public DateTime Date { get; set; }
        public float Result { get; set; }
        public bool IsPassed { get; set; }
        public string? Notes { get; set; }
        public string EmployeeName { get; set; }
        public List<SubjectMarkResponseDto> SubjectMarks { get; set; }
    }

}
