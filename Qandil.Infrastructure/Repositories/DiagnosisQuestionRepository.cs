using Microsoft.EntityFrameworkCore;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class DiagnosisQuestionRepository(ApplicationDbContext _context) : BaseRepository<DiagnosisQuestion>(_context), IDiagnosisQuestionRepository
    {
       
        

    }
}
