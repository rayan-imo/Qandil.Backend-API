using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class UserOtpRepository(ApplicationDbContext _context):BaseRepository<UserOtp>(_context),IUserOtpRepository
    {
    }
}
