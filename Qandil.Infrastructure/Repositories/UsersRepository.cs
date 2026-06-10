using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class UsersRepository(ApplicationDbContext _context):BaseRepository<User>(_context),IUsersRepository
    {
    }
}
