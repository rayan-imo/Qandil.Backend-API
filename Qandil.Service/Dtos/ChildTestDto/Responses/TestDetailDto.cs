namespace Qandil.Service.Dtos.ChildTestDto.Responses
{
    public class TestDetailDto
    {
        public DateTime Date { get; set; }
        public float Result { get; set; }
        public bool IsPassed { get; set; }
        public string? Notes { get; set; }
        public string EmployeeName { get; set; }
    }

}
