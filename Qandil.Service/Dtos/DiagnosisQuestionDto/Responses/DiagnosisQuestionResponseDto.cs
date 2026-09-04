using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.DiagnosisQuestionDto.Responses
{
    public class DiagnosisQuestionResponseDto
    {
        public string SubTitle { get; set; }  // ← العنوان الفرعي (تاريخ طبي، نمو نفسي، ...)
        public List<DiagnosisQuestionDto> Questions { get; set; } = new();
    }
}
