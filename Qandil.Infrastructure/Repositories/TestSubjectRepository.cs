using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class TestSubjectRepository(ApplicationDbContext _context):BaseRepository<TestSubject>(_context),ITestSubjectRepository
    {
        
    }
}
