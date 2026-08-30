namespace Qandil.API.Dtos.Requests.ChildTestSubjectMark
{
    public class ChildTestSubjectMarkRequest
    {
        public float ObtainMark { get; set; }
        public string? Notes { get; set; }
        public Guid SubjectId { get; set; }
       

    }
}
