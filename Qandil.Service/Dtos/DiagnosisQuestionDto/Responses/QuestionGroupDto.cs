namespace Qandil.Service.Dtos.QuestionDto.Responses
{
    public class QuestionGroupDto
    {
        public string? SubTitle { get; set; }
        public List<QuestionWithAnswerDto> Questions { get; set; }
    }
}
