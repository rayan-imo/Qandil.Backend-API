using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class EvaluationCardRepository(ApplicationDbContext _context) : BaseRepository<EvaluationCard>(_context), 
        IEvaluationCardRepository
    {
    }
}
