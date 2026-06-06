using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class ClassroomRepository(ApplicationDbContext _context):BaseRepository<Classroom>(_context),IClassroomRepository
    {
        
    }
}
