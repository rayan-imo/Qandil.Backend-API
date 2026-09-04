using Qandil.Core.Enums;
using Qandil.Service.Dtos.QuestionOptionsDto.Responses;

namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Responses
{
    public class DiagnosisQuestionDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public int Order { get; set; }
        public List<DiagnosisOptionDto>? Options { get; set; } = new();
    }
}
