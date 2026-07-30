using Qandil.Core.Entity;

namespace Qandil.Core.Interfacres
{
    public interface IQuestionRepository:IBaseRepository<DiagnosisQuestion>
    {
        public Task<Dictionary<string, Dictionary<string, List<DiagnosisQuestion>>>> GetGroupedQuestionsAsync();
        public  Task<List<DiagnosisQuestion>> GetDiagnosisQuestionsAsync();
    }
}
