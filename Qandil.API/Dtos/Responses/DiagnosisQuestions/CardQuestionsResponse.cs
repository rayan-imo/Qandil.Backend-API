using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class CardQuestionsResponse
    {
        public CardType? CardType { get; set; }
        public string DisplayName { get; set; }
        public List<SubTitleGroupResponse> SubTitleGroups { get; set; } = new();
    }
}
