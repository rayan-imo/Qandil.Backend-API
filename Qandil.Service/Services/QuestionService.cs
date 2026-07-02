using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.IServices;

namespace Qandil.Service.Services
{
    public class QuestionService(IUnitOfWork _uof) : IQuestionService
    {

        public async Task<Result<List<Question>>> GetCardQuestionsAsync()
        {
            //هاد التابع بجيب أسئلة البطاقات ومجمعة بحسب العنوان الرئيسي      
            var spec = BaseSpecification<Question>.Create()

                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardName != null)
                .OrderByAsc(q => q.Order);

            var CardQuestions = await _uof.QuestionRepository.ListAsync(spec);


            return Result<List<Question>>.Success(CardQuestions);

        }

        public async Task<Result<List<Question>>> GetQuestionsByCardName(string cardName)
        {

            var spec = BaseSpecification<Question>.Create()

                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardName == cardName)
                .OrderByAsc(q => q.Order);

            var CardQuestions = await _uof.QuestionRepository.ListAsync(spec);

            return Result<List<Question>>.Success(CardQuestions);

        }
        public async Task<Result<List<Question>>> GetDiagnosisQuestions()
        {

            var DiagnosisQuestion = await _uof.QuestionRepository.GetDiagnosisQuestionsAsync();

            return Result<List<Question>>.Success(DiagnosisQuestion);

        }

        public async Task<Result<List<Question>>> GetAllQuestionsAsync()
        {
            var spec = BaseSpecification<Question>.Create()
                .Where(q => q.DeletedAt == null)
                .OrderByAsc(q => q.Order);

            return Result<List<Question>>.Success(await _uof.QuestionRepository.ListAsync(spec));
        }


    }
}
