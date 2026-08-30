namespace Qandil.Service.Dtos.ChildTestDto.Responses
{
    public class AttemptDto
    {
        public int AttemptNumber { get; set; }
        public TestDetailDto? PreTest { get; set; }
        public TestDetailDto? PostTest { get; set; }
    }

}
