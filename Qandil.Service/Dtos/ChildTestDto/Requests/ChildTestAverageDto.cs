namespace Qandil.Service.Dtos.ChildTestDto.Requests
{
    public class ChildTestAverageDto
    {
        public int AttemptNumber { get; set; }
        public DateTime DateTime { get; set; }
        public double? Average {  get; set; }
        public ICollection<ChildSubjectMarkDto> Marks { get; set; }

    }


}
