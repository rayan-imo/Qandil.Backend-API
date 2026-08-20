using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class SubjectRepository(ApplicationDbContext _context):BaseRepository<Subject>(_context),ISubjectRepository
    {
    }
}
