using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.Service.Services
{
    public class DiagnosisQuestionService(IUnitOfWork _uow) : IDiagnosisQuestionService
    {

      

        public async Task<Result<List<DiagnosisQuestion>>> GetQuestionsByCardName(string cardName)
        {

            var spec = BaseSpecification<DiagnosisQuestion>.Create()

                .Where(q => q.DeletedAt == null)
                .AndFilter(q => q.CardName == cardName)
                .OrderByAsc(q => q.Order);

            var cardQuestions = await _uow.QuestionRepository.ListAsync(spec);

            if (cardQuestions == null || !cardQuestions.Any())

                return Result<List<DiagnosisQuestion>>.Failure($"There are no questions for this card {cardName}");

            return Result<List<DiagnosisQuestion>>.Success(cardQuestions);

        }
        public async Task<Result<List<DiagnosisQuestion>>> GetDiagnosisQuestions()
        {

            var diagnosisQuestion = await _uow.QuestionRepository.GetDiagnosisQuestionsAsync();
            
            if(diagnosisQuestion == null || !diagnosisQuestion.Any())
                return Result<List<DiagnosisQuestion>>.Failure($"There are no questions ");


            return Result<List<DiagnosisQuestion>>.Success(diagnosisQuestion);

        }

        public async Task<Result<DiagnosisQuestion>> GetQuestionByIdAsync(Guid id)
        {
            var question = await _uow.QuestionRepository.GetByIdAsync(id);

            if (question == null)
                return Result<DiagnosisQuestion>.Failure("السؤال غير موجود");

            return Result<DiagnosisQuestion>.Success(question);
        }


        public async Task<Result<DiagnosisQuestion>> AddQuestionAsync(QuestionRequestDto dto)
        {
            var question = new DiagnosisQuestion
            {
                Id = Guid.NewGuid(),
                CardName = dto.CardName,
                MainTitle = dto.MainTitle,
                SubTitle = dto.SubTitle,
                QuestionText = dto.QuestionText,
                Type = dto.Type,
                Options = dto.Options,
                Order = dto.Order
            };

            await _uow.QuestionRepository.AddAsync(question);
            await _uow.CompleteAsync();

            return Result<DiagnosisQuestion>.Success(question);
        }

        public async Task<Result<DiagnosisQuestion>> UpdateQuestionAsync(Guid id, QuestionRequestDto dto)
        {
            var question = await _uow.QuestionRepository.GetByIdAsync(id);
            if (question == null)
                return Result<DiagnosisQuestion>.Failure("السؤال غير موجود");

            question.CardName = dto.CardName;
            question.MainTitle = dto.MainTitle;
            question.SubTitle = dto.SubTitle;
            question.QuestionText = dto.QuestionText;
            question.Type = dto.Type;
            question.Options = dto.Options;
            question.Order = dto.Order;

            await _uow.QuestionRepository.UpdateAsync(question);
            await _uow.CompleteAsync();

            return Result<DiagnosisQuestion>.Success(question);
        }

        public async Task<Result<bool>> DeleteQuestionAsync(Guid id)
        {
            var question = await _uow.QuestionRepository.GetByIdAsync(id);
            if (question == null)
                return Result<bool>.Failure("السؤال غير موجود");

            question.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }




    }
}
