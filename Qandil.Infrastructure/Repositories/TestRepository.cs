using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class TestRepository(ApplicationDbContext _context):BaseRepository<Test>(_context),ITestRepository
    {
        public async Task<List<Guid>> GetSubjectIdsByTestIdAsync(Guid testId)
        {
            return await _context.TestSubjects
                .Where(ts => ts.TestId == testId)
                .Select(ts => ts.SubjectId)
                .ToListAsync();
        }
    }
}
