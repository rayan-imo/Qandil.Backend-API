namespace Qandil.Service.Dtos.QuestionDto.Responses
{
    public class QuestionWithAnswerDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public object? Answer { get; set; }
    }
}
