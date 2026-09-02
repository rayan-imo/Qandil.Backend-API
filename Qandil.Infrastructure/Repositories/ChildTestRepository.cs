using Microsoft.EntityFrameworkCore;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;                                              
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class ChildTestRepository(ApplicationDbContext _context):BaseRepository<ChildTest>(_context),IChildTestRepository
    {
        public async Task<ChildTest?> GetLastAttemptTestInfoAsync(Guid childId, Guid testId)
        {
            return await _context.ChildTests
                .Where(ct => ct.ChildId == childId
                          && ct.TestId == testId
                          && ct.DeletedAt == null)
                .Include(ct => ct.Test)
                .Include(ct => ct.ChildTestSubjectMarks)
                    .ThenInclude(ctsm => ctsm.Subject)
                .OrderByDescending(ct => ct.AttemptNumber)
                .ThenByDescending(ct => ct.Date)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsForAttemptAsync(Guid childId, Guid levelId, TestType type, int attemptNumber)
        {
            return await _context.ChildTests
                .AnyAsync(ct => ct.ChildId == childId
                             && ct.Test.LevelId == levelId
                             && ct.Type == type
                             && ct.AttemptNumber == attemptNumber
                             && ct.DeletedAt == null);
        }


    }
}
