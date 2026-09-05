using Qandil.Core.Common;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.AnswerDto.Requests;
using Qandil.Service.Dtos.DiagnisisAnswerDto.Requests;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Responses;

namespace Qandil.Service.IServices
{
    public interface IAnswerService
    {

        Task<Result<CardResultDto>> SaveAndEvaluateCardAsync(EvaluateCardRequestDto dto);
        Task<Result<Guid>> SaveDiagnosisSubTitleAnswersAsync(SaveDiagnosisSubTitleAnswersRequestDto dto);
        Task<Result<List<CardResultDto>>> GetCardResultsAsync(Guid diagnosisId);
        Task<Result<CardDetailsResponseDto>> GetCardAnswerDetailsAsync(Guid diagnosisId, CardType cardType);
        Task<Result<List<DiagnosisSubTitleResultDto>>> GetDiagnosisQuestionsResultsAsync(Guid diagnosisId);
        Task<Result<bool>> UpdateAnswerAsync(Guid answerId, UpdateAnswerRequestDto dto);
        Task<Result<bool>> DeleteAnswerByQuestionAsync(Guid diagnosisId, Guid questionId);

    }
}
