using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class QuestionOptionRepository(ApplicationDbContext _context) : BaseRepository<QuestionOption>(_context), IQuestionOptionRepository
    {
         
    }
}
