using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.Core.Interfacres
{
    public interface IChildTestRepositoy : IBaseRepository<ChildTest>
    {
        public Task<bool> ExistsForAttemptAsync(Guid childId, Guid levelId, TestType type, int attemptNumber);
        public Task<ChildTest?> GetLastAttemptTestInfoAsync(Guid childId, Guid testId);
    }

}
