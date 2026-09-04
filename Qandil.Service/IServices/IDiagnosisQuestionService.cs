using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Requests;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Responses;
using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.Dtos.QuestionOptionsDto.Responses;

namespace Qandil.Service.IServices
{
    public interface IDiagnosisQuestionService
    {

        public Task<Result<List<CardQuestionsResponseDto>>> GetAllCardQuestionsAsync();
        public Task<Result<DiagnosisQuestion>> UpdateCardQuestionAsync(Guid id, CardQuestionRequestDto dto);
        Task<Result<DiagnosisQuestion>> AddCardQuestionAsync(CardQuestionRequestDto dto);
        Task<Result<DiagnosisQuestion>> GetQuestionByIdAsync(Guid id);
        public Task<Result<List<DiagnosisQuestion>>> GetQuestionsByCardType(CardType cardType);
        Task<Result<DiagnosisQuestion>> AddDiagnosisQuestionAsync(DiagnosisQuestionRequestDto dto);
        Task<Result<List<DiagnosisQuestionResponseDto>>> GetAllDiagnosisQuestionsAsync();
        public Task<Result<DiagnosisQuestion>> UpdateDiagnosisQuestionAsync(Guid id, DiagnosisQuestionRequestDto dto);
        Task<Result<bool>> DeleteQuestionAsync(Guid id);

    }
}
