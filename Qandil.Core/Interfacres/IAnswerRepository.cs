using Qandil.Core.Entity;

namespace Qandil.Core.Interfacres
{
    public interface IAnswerRepository:IBaseRepository<Answer> 
    {
       public Task<List<Answer>> GetAnswersByDiagnosisIdAsync(Guid diagnosisId);
    }
}
