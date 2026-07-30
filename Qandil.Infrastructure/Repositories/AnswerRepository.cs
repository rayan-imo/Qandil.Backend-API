using Microsoft.EntityFrameworkCore;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class AnswerRepository(ApplicationDbContext _context) : BaseRepository<DiagnosisAnswer>(_context), IAnswerRepository
    {
        public async Task<List<DiagnosisAnswer>> GetAnswersByDiagnosisIdAsync(Guid diagnosisId)
        {
            return await _context.Set<DiagnosisAnswer>()
                .Where(a => a.DiagnosisId == diagnosisId && a.DeletedAt == null)
                .ToListAsync();
        }
    }
}
