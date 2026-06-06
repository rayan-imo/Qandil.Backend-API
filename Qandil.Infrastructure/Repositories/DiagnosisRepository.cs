using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class DiagnosisRepository(ApplicationDbContext _context):BaseRepository<Diagnosis>(_context),IDiagnosisRepository
    {

    }
}
