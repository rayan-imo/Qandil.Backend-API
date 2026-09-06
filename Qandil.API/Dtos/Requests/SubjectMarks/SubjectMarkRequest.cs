namespace Qandil.API.Dtos.Requests.SubjectMarks
{
    public class SubjectMarkRequest
    {
        public float ObtainMark { get; set; }
        public Guid ChildTestId { get; set; }
        public Guid SubjectId { get; set; }
        public string? Notes { get; set; }
    }
}
