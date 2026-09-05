using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.DiagnosisDto.Requests;
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
            var child = await _uow.ChildRepository.GetByIdAsync(dto.ChildId);
            if (child == null)
                return Result<Diagnosis>.Failure("الطفل غير موجود ");

            var hasExistingDiagnosis = await _uow.DiagnosisRepository
     .AnyAsync(d => d.ChildId == dto.ChildId && d.DeletedAt == null);

            if (hasExistingDiagnosis == true)
                return Result<Diagnosis>.Failure("لا يمكن الإضافة، هذا الطفل لديه تشخيص مسبقاً");

            var employee =await _uow.EmployeeRepository.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                return Result<Diagnosis>.Failure("الموظف غير موجود");
            var diagnosis = new Diagnosis
            {
                Id = Guid.NewGuid(),
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

        
        

    }
}
