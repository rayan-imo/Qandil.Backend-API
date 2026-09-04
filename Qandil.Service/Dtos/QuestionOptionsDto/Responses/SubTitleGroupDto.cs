namespace Qandil.Service.Dtos.QuestionOptionsDto.Responses
{
    public class SubTitleGroupDto
    {
        public string SubTitle { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }

}
