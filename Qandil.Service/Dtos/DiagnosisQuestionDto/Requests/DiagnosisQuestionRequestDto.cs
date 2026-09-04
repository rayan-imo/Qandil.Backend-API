using Qandil.Core.Enums;
using Qandil.Service.Dtos.QuestionOptionsDto.Requests;

namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Requests
{
    public class DiagnosisQuestionRequestDto
    {
        public string SubTitle { get; set; }
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
        public int Order { get; set; }
        public List<DiagnosisQuestionOptionRequestDto>? Options { get; set; }
    }
}
