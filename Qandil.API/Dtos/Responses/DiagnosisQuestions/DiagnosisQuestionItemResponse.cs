using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.DiagnosisQuestions
{
    public class DiagnosisQuestionItemResponse
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public int Order { get; set; }
        public List<DiagnosisOptionResponse> Options { get; set; } = new();

        public static DiagnosisQuestionItemResponse Transform(DiagnosisQuestion diagnosisQuestion)
        {
            return new DiagnosisQuestionItemResponse()
            {
                Id = diagnosisQuestion.Id,
                QuestionText = diagnosisQuestion.QuestionText,
                Type = diagnosisQuestion.Type,
                Order = diagnosisQuestion.Order,
                Options = diagnosisQuestion.QuestionOptions?
                .Select(o => new DiagnosisOptionResponse
                {
                    Id = o.Id,
                    Text = o.Text,
                }).ToList() ?? new List<DiagnosisOptionResponse>()


            };
        }
    }

}
