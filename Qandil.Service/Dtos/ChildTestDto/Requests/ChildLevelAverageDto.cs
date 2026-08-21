namespace Qandil.Service.Dtos.ChildTestDto.Requests
{
    public class ChildLevelAverageDto
    {
        public Guid LevelId { get; set; }
      public ICollection<ChildTestAverageDto> PreTests { get; set; }
      public ICollection<ChildTestAverageDto> ProTests { get; set; }

    }
}
