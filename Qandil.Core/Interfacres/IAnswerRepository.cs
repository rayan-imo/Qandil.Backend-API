using Qandil.Core.Entity;

namespace Qandil.Core.Interfacres
{
    public interface IAnswerRepository:IBaseRepository<DiagnosisAnswer> 
    {
       public Task<List<DiagnosisAnswer>> GetAnswersByDiagnosisIdAsync(Guid diagnosisId);
    }
}
