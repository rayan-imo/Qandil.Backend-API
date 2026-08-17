using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class ChildTestSubjectMarkRepository(ApplicationDbContext _context):BaseRepository<ChildTestSubjectMark>(_context),IChildTestSubjectMarkRepositoy
    {
        
    }
}
