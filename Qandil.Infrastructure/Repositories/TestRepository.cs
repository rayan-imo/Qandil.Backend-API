using FluentValidation.Validators;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class TestRepository(ApplicationDbContext _context):BaseRepository<Test>(_context),ITestRepository
    {
    }
}
