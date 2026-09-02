namespace Qandil.Service.Dtos.ChildDto.Response
{
    public class ChildPostTestResponseDto
    {
        public Guid ChildId { get; set; }
        public required string ChildName { get; set; }
        public required string MotherName{get; set; }
        public required string FatherName {  get; set; }
        public DateTime JoiningDate { get; set; }
        public string? ProgramName { get; set; }
        public PostTestResponseDto? LatestTest { get; set; }
    }
}
  


