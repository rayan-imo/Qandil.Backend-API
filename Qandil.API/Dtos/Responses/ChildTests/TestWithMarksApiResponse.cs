namespace Qandil.API.Dtos.Responses.ChildTests
{
    public class TestWithMarksApiResponse
    {
        public Guid ChildTestId { get; set; }
        public DateTime Date { get; set; }
        public float Result { get; set; }
        public bool IsPassed { get; set; }
        public string? Notes { get; set; }
        public string? EmployeeName { get; set; }
        public List<SubjectMarkApiResponse> SubjectMarks { get; set; }
    }
}
