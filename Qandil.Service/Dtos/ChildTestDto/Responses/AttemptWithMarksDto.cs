namespace Qandil.Service.Dtos.ChildTestDto.Responses
{
    public class AttemptWithMarksDto
    {
        public int AttemptNumber { get; set; }
        public TestWithMarksResponseDto? PreTest { get; set; }
        public TestWithMarksResponseDto? PostTest { get; set; }
    }

}
