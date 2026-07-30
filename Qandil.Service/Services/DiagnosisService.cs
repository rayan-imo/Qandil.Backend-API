using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.DiagnosisDto.Requests;
using Qandil.Service.Dtos.DiagnosisDto.Response;
using Qandil.Service.Dtos.QuestionDto.Requests;
using Qandil.Service.Dtos.QuestionDto.Responses;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Diagnosis;

namespace Qandil.Service.Services
{
    public class DiagnosisService(IUnitOfWork _uow) : IDiagnosisService
    {

        public async Task<Result<PagedResult<Diagnosis>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spac = BaseSpecification<Diagnosis>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Diagnosis>>.Success(await _uow.DiagnosisRepository.PagedListAsync(spac));

        }


        public async Task<Result<Diagnosis>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Diagnosis>.Failure("Diagnosis ID cannot be empty.");

            var diagnosis = await _uow.DiagnosisRepository.GetByIdAsync(id);

            if (diagnosis == null || diagnosis.DeletedAt != null)
                return Result<Diagnosis>.Failure($"Diagnosis with ID was not found.");

            return Result<Diagnosis>.Success(diagnosis);
        }

        public async Task<Result<Diagnosis>> AddAsync(DiagnosisRequestDto dto)
        {
            await new DiagnosisValidator().ValidateAndThrowAsync(dto);
            var child = _uow.ChildRepository.GetById(dto.ChildId);
            if (child == null)
                return Result<Diagnosis>.Failure("الطفل غير موجود ");
            var employee= _uow.EmployeeRepository.GetById(dto.EmployeeId);
            if (employee == null)
                return Result<Diagnosis>.Failure("الموظف غير موجود");
            var diagnosis = new Diagnosis
            {
                Id=Guid.NewGuid(),
                DisabilityOnsetDate = dto.DisabilityOnsetDate,
                MedicalNots = dto.MedicalNots,
                ChildId = dto.ChildId,
                EmployeeId = dto.EmployeeId,

            };

            await _uow.DiagnosisRepository.AddAsync(diagnosis);
            await _uow.CompleteAsync();
            return Result<Diagnosis>.Success(diagnosis);


        }

        public async Task<Result<Diagnosis>> UpdateAsync(DiagnosisRequestDto dto, Guid id)
        {

            if (id == Guid.Empty)
                return Result<Diagnosis>.Failure("Diagnosis ID cannot be empty.");

            var diagnosis = await _uow.DiagnosisRepository.GetByIdAsync(id);

            if (diagnosis == null || diagnosis.DeletedAt != null)
                return Result<Diagnosis>.Failure($"Diagnosis with ID was not found.");
            await new DiagnosisValidator().ValidateAndThrowAsync(dto);

            diagnosis.DisabilityOnsetDate = dto.DisabilityOnsetDate;
            diagnosis.MedicalNots = dto.MedicalNots;
            diagnosis.ChildId = dto.ChildId;
            diagnosis.EmployeeId = dto.EmployeeId;
            await _uow.DiagnosisRepository.UpdateAsync(diagnosis);
            await _uow.CompleteAsync();
            return Result<Diagnosis>.Success(diagnosis);

        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Diagnosis ID cannot be empty.");

            var diagnosis = await _uow.DiagnosisRepository.GetByIdAsync(id);

            if (diagnosis == null || diagnosis.DeletedAt != null)
                return Result<bool>.Failure("Diagnosis with iD was not found ");

            diagnosis.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);

        }

        public async Task<Result<Guid>> CreateDiagnosisWithAnswersAsync(CreateDiagnosisWithAnswersDto dto)
        {
            var diagnosis = new Diagnosis
            {
                Id = Guid.NewGuid(),
                DisabilityOnsetDate = dto.DisabilityOnsetDate,
                MedicalNots = dto.MedicalNots,
                ChildId = dto.ChildId,
                EmployeeId = dto.EmployeeId
            };

            await _uow.DiagnosisRepository.AddAsync(diagnosis);

            foreach (var answerDto in dto.Answers)
            {
                var answer = new DiagnosisAnswer
                {
                    DiagnosisId = diagnosis.Id,
                    QuestionId = answerDto.QuestionId,

                    BooleanValue = answerDto.BooleanValue,
                    ScoreValue = answerDto.ScoreValue,
                    TextValue = answerDto.TextValue,
                    SelectedOption = answerDto.SelectedOption,
                    Notes = answerDto.Notes
                };

                await _uow.AnswerRepository.AddAsync(answer);
            }

            await _uow.CompleteAsync();

            return Result<Guid>.Success(diagnosis.Id);
        }

       
        public async Task<Result<FullDiagnosisResponseDto>> GetFullDiagnosisAsync(Guid diagnosisId)
        {
            if (diagnosisId == Guid.Empty)
            {
                return Result<FullDiagnosisResponseDto>.Failure("Diagnosis Id ID cannot be empty.");
            }

            var diagnosis = await _uow.DiagnosisRepository.GetByIdAsync(diagnosisId);
            if (diagnosis == null)
                return Result<FullDiagnosisResponseDto>.Failure("No diagnosis found for this Id");

            // جلب جميع الإجابات
            var Answers = await _uow.AnswerRepository.GetAnswersByDiagnosisIdAsync(diagnosisId);

            if (Answers == null)
                return Result<FullDiagnosisResponseDto>.Failure("No answer found for this Diagnosis");

            // جلب أسئلة التشخيص مع إجاباتها
            var diagnosisQuestions = await _uow.QuestionRepository.GetDiagnosisQuestionsAsync();

            var diagnosisQuestionsWithAnswers = diagnosisQuestions
                .GroupBy(q => q.SubTitle ?? "أسئلة أساسية")
                .Select(g => new QuestionGroupDto
                {
                    SubTitle = g.Key,

                    Questions = g.OrderBy(q => q.Order).Select(q => new QuestionWithAnswerDto
                    {
                        Id = q.Id,
                        Text = q.QuestionText,
                        Answer = GetAnswerValue(Answers.FirstOrDefault(a => a.QuestionId == q.Id), q.Type),
                    }).ToList()
                }).ToList();

            // جلب نتائج التقييم
            var evaluationResults = await _uow.EvaluationCardRepository
                .FindAllAsync(e => e.DiagnosisId == diagnosisId && e.DeletedAt == null);

            var evaluationDtos = evaluationResults.Select(e => new EvaluationCardDto
            {
                CardName = e.CardName,
                MainTitleScores = e.MainTitleScores,
                TotalScore = e.TotalScore,
                EvaluationMessage = e.EvaluationMessage
            }).ToList();

            var result = new FullDiagnosisResponseDto
            {
                DiagnosisId = diagnosis.Id,
                ChildId = diagnosis.ChildId,  // ← بس الـ ID
                DiagnosisQuestions = diagnosisQuestionsWithAnswers,
                Evaluations = evaluationDtos
            };
            return Result<FullDiagnosisResponseDto>.Success(result);

        }

        private object? GetAnswerValue(DiagnosisAnswer? answer, QuestionType type)
        {
            if (answer == null) return null;

            return type switch
            {
                QuestionType.Boolean => answer.BooleanValue,
                QuestionType.Score => answer.ScoreValue,
                QuestionType.Text => answer.TextValue,
                QuestionType.Options => answer.SelectedOption,
                _ => null
            };
        }

    }
}
