namespace Qandil.Service.Dtos.ChildTestDto.Responses
{
    public class TestAttemptsResponseDto
    {
        public string TestName { get; set; }
        public string LevelName { get; set; }
        public List<AttemptWithMarksDto> Attempts { get; set; }
    }
}
