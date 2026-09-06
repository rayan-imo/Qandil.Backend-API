using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class SubjectMarkRepository(ApplicationDbContext _context) : BaseRepository<SubjectMark>(_context), ISubjectMarkRepository
    {

    }
}
