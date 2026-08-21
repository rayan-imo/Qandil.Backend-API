namespace Qandil.Service.Dtos.ChildTestDto.Requests
{
    public class ChildTestsDto
    {
        public ICollection<ChildTestAverageDto> PreTests { get; set; }
        public ICollection<ChildTestAverageDto> ProTests { get; set; }

    }


}
