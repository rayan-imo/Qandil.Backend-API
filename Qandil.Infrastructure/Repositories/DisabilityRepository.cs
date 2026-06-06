using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class DisabilityRepository(ApplicationDbContext _context):BaseRepository<Disability>(_context),IDisabilityRepository
    {

    }
}
