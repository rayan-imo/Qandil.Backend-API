using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class LevelRepository(ApplicationDbContext _context):BaseRepository<Level>(_context),ILevelRepository
    {
    }
}
