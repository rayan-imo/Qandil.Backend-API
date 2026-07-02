namespace Qandil.API.Dtos.Requests.Answer
{
    public class AnswerRequest
    {
        public Guid DiagnosisId { get; set; }
        public Guid QuestionId { get; set; }


        public bool? BooleanValue { get; set; }       // نعم/لا
        public int? ScoreValue { get; set; }          // 0,1,2,3
        public string? TextValue { get; set; }        // نص
        public string? SelectedOption { get; set; }   // للخيارات المتعددة

        public string? Notes { get; set; }
    }
}

