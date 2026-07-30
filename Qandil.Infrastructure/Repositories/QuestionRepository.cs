using Microsoft.EntityFrameworkCore;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;

namespace Qandil.Infrastructure.Repositories
{
    public class QuestionRepository(ApplicationDbContext _context) : BaseRepository<DiagnosisQuestion>(_context), IQuestionRepository
    {
        public async Task<Dictionary<string, Dictionary<string, List<DiagnosisQuestion>>>> GetGroupedQuestionsAsync()
        {

            var questions = await _context.Set<DiagnosisQuestion>()
                 .Where(q => q.DeletedAt == null)
                 .OrderBy(q => q.Order)

                 .ToListAsync();

            return questions
                .GroupBy(q => q.MainTitle)
                .ToDictionary(g => g.Key, g => g.ToList().GroupBy(q => q.SubTitle)
                .ToDictionary(g => g.Key, g => g.ToList()));


        }

        public async Task<List<DiagnosisQuestion>> GetDiagnosisQuestionsAsync()
        {
            var diagnosisQuestions = await _context.Set<DiagnosisQuestion>()
                .Where(q => q.DeletedAt == null && q.CardName == null)
                .OrderBy(q => q.Order)
                .ToListAsync();

            return diagnosisQuestions;
        }

        

    }
}
