using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Responses
{
    public class DiagnosisAnswerDetailDto
    {
        public Guid? AnswerId { get; set; } 
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public bool? BooleanValue { get; set; }
        public string TextValue { get; set; }
        public string SelectedOptionText { get; set; }   // بيتحل من QuestionOption لو النوع  Options
    }

}
