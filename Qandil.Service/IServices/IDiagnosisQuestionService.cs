using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.Dtos.QuestionDto.Responses;

namespace Qandil.Service.IServices
{
    public interface IDiagnosisQuestionService
    {
        public Task<Result<List<DiagnosisQuestion>>> GetQuestionsByCardName(string cardName);
        public Task<Result<List<DiagnosisQuestion>>> GetDiagnosisQuestions();
        Task<Result<DiagnosisQuestion>> GetQuestionByIdAsync(Guid id);
        Task<Result<DiagnosisQuestion>> AddQuestionAsync(QuestionRequestDto dto);
        Task<Result<DiagnosisQuestion>> UpdateQuestionAsync(Guid id, QuestionRequestDto dto);
        Task<Result<bool>> DeleteQuestionAsync(Guid id);

    }
}
