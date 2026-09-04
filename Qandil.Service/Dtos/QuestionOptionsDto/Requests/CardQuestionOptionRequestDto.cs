using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.QuestionOptionsDto.Requests
{
    public class CardQuestionOptionRequestDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int? Value { get; set; }
        public int Order { get; set; }
        

    }
}
