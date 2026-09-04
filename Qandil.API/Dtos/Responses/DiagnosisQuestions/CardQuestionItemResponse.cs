using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class CardQuestionItemResponse
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public ScoreInputType? ScoreInputType { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public static CardQuestionItemResponse Transform(DiagnosisQuestion diagnosisQuestions)
        {
            return new CardQuestionItemResponse
            {
                Id = diagnosisQuestions.Id,
                QuestionText = diagnosisQuestions.QuestionText,
                Type = diagnosisQuestions.Type,
                ScoreInputType = diagnosisQuestions.ScoreInputType,
                MinValue = diagnosisQuestions.MinValue,
                MaxValue = diagnosisQuestions.MaxValue,
            };
        }

    }
}
