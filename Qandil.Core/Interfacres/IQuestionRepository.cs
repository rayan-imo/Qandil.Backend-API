using Qandil.Core.Entity;

namespace Qandil.Core.Interfacres
{
    public interface IQuestionRepository:IBaseRepository<Question>
    {
        public Task<List<Question>> GetQuestionsByMainTitleAsync(string mainTitle);

        public Task<Dictionary<string, Dictionary<string, List<Question>>>> GetGroupedQuestionsAsync();
        public  Task<List<Question>> GetDiagnosisQuestionsAsync();
    }
}
