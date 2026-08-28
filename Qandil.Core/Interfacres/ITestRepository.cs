using Qandil.Core.Entity;

namespace Qandil.Core.Interfacres
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        public  Task<List<Guid>> GetSubjectIdsByTestIdAsync(Guid testId);


    }

}
