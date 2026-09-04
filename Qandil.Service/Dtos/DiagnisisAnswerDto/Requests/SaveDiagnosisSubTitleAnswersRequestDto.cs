namespace Qandil.Service.Dtos.AnswerDto.Requests
{
    public class SaveDiagnosisSubTitleAnswersRequestDto
    {
        public Guid DiagnosisId { get; set; }
        public string SubTitle { get; set; }        // إحدى المجموعات الخمس
        public List<AnswerRequestDto> Answers { get; set; }
    }


}
