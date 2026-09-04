namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Responses
{
    public class DiagnosisSubTitleResultDto
    {
        public string SubTitle { get; set; }
        public List<DiagnosisAnswerDetailDto> Answers { get; set; } = new();
    }

}
