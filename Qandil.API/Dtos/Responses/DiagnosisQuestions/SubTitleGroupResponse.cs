namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class SubTitleGroupResponse
    {
        public string SubTitle { get; set; }
        public List<CardQuestionItemResponse> Questions { get; set; } = new();
    }

}
