using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.Dtos.QuestionDto.Requests;

namespace Qandil.Service.IServices
{
    public interface IAnswerService
    {
        public  Task<Result<PagedResult<DiagnosisAnswer>>> GetAnswersByDiagnosisId(Guid id);
        public  Task<Result<Guid>> SaveDiagnosisAnswersAsync(Guid diagnosisId, List<AnswerRequestDto> answers);
        public Task<Result<EvaluationCard>> SaveAndEvaluateCardAsync(EvaluateCardRequestDto dto);
        public  Task<Result<EvaluationCard>> CalculateEvaluationForCard(List<DiagnosisAnswer> answers, string cardName);

      
       
    }
}
