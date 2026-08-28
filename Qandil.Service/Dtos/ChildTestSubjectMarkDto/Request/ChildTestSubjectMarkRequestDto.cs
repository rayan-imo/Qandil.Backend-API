namespace Qandil.Service.Dtos.ChildTestSubjectMarkDto.Request
{
    public class ChildTestSubjectMarkRequestDto
    {
        public float ObtainMark { get; set; }
        public string? Notes { get; set; }
        public Guid SubjectId { get; set; }
      
    }
}

