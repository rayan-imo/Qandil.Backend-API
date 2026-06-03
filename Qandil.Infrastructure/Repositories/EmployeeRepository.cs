using Microsoft.AspNetCore.Identity.Data;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class EmployeeRepository(ApplicationDbContext _context):BaseRepository<Employee>(_context),IEmployeeRepository
    {
    }
}
