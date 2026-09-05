namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Responses
{
    public class CardAnswerDetailDto
    {
        public Guid? AnswerId { get; set; }
        public Guid QuistionId { get; set; }
        public string QuestionText { get; set; }
        public string SubTitle { get; set; }
        public int? ScoreValue { get; set; }
        public string DisplayAnswer { get; set; }   // "دائماً" لو Frequency، أو الرقم نفسه لو RawNumber، أو "لم تتم الإجابة"
    }

}
