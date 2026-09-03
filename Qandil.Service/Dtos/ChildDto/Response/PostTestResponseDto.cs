namespace Qandil.Service.Dtos.ChildDto.Response
{
    public class PreTestResponseDto
    {
        public Guid TestId { get; set; }
        public string TestName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public float Result { get; set; }
        public bool IsPassed { get; set; }
        public List<SubjectMarkResponseDto> SubjectMarks { get; set; } = new();
    }
}
  


