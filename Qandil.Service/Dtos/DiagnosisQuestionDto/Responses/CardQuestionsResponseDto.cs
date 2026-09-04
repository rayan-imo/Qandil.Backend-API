using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.QuestionOptionsDto.Responses
{
    public class CardQuestionsResponseDto
    {
        public CardType? CardType { get; set; }
        public string DisplayName { get; set; }
        public List<SubTitleGroupDto> SubTitleGroups { get; set; } = new();
    }
 
}
