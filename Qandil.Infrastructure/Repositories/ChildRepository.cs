using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class ChildRepository(ApplicationDbContext _context):BaseRepository<Child>(_context),IChildRepository
    {
    }
}
