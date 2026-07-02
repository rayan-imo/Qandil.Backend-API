using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.QuestionDto.Responses;

namespace Qandil.Service.IServices
{
    public interface IQuestionService
    {
        public  Task<Result<List<Question>>> GetCardQuestionsAsync();
        public Task<Result<List<Question>>> GetQuestionsByCardName(string cardName);
        public Task<Result<List<Question>>> GetDiagnosisQuestions();
        public  Task<Result<List<Question>>> GetAllQuestionsAsync();

    }
}
