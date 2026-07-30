using Qandil.Core.Entity;
using Qandil.Service.Dtos.QuestionDto.Responses;

namespace Qandil.Service.Dtos.DiagnosisDto.Response
{
    public class DiagnosisDetailsDto
    {
        public Diagnosis Diagnosis { get; set; }

        public List<DiagnosisAnswer> Answers { get; set; }

        public List<EvaluationCard> Evaluations { get; set; }
    }
}
