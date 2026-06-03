using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class ProgramRepository(ApplicationDbContext _context):BaseRepository<Program>(_context),IProgramRepositoy
    {
    }
}
