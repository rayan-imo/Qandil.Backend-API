using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class ChildTestRepository(ApplicationDbContext _context):BaseRepository<ChildTest>(_context),IChildTestRepositoy
    {
    }
}
