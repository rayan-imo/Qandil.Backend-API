using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class SchoolRepository(ApplicationDbContext _context):BaseRepository<School>(_context),ISchoolRepository
    {
    }
}
