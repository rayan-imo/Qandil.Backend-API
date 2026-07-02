using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Answers
{
    public class AnswerResponse
    {
        public Guid Id { get; set; }
        public Guid DiagnosisId { get; set; }
      
        public Guid QuestionId { get; set; }
      
   
        public bool? BooleanValue { get; set; }       // نعم/لا
        public int? ScoreValue { get; set; }          // 0,1,2,3
        public string? TextValue { get; set; }        // نص
        public string? SelectedOption { get; set; }   // للخيارات المتعددة

        public string? Notes { get; set; }

        public static AnswerResponse Transform(Answer answer)
        {
            return new AnswerResponse
            {
                Id = answer.Id,
                DiagnosisId = answer.DiagnosisId,
                BooleanValue = answer.BooleanValue,
                TextValue = answer.TextValue,
                SelectedOption = answer.SelectedOption,
                Notes = answer.Notes,

            };
        }

    }
}
