using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.Diagnosis.Requests;
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

        public async Task<Result<Guid>> AddAsync(DiagnosisRequestDto dto)
        {
            await new DiagnosisValidator().ValidateAndThrowAsync(dto);
            var diagnosis = new Diagnosis
            {
                DisabilityOnsetDate = dto.DisabilityOnsetDate,
                MedicalNots = dto.MedicalNots,
                StatusDescription = dto.StatusDescription,
                ChildId = dto.ChildId,
                EmployeeId = dto.EmployeeId,

            };

            await _uow.DiagnosisRepository.AddAsync(diagnosis);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(diagnosis.Id);


        }

        public async Task<Result<Guid>> UpdateAsync(DiagnosisRequestDto dto, Guid id)
        {

            if (id == Guid.Empty)
                return Result<Guid>.Failure("Diagnosis ID cannot be empty.");

            var diagnosis = await _uow.DiagnosisRepository.GetByIdAsync(id);

            if (diagnosis == null || diagnosis.DeletedAt != null)
                return Result<Guid>.Failure($"Diagnosis with ID was not found.");
            await new DiagnosisValidator().ValidateAndThrowAsync(dto);

            diagnosis.DisabilityOnsetDate = dto.DisabilityOnsetDate;
            diagnosis.MedicalNots = dto.MedicalNots;
            diagnosis.StatusDescription = dto.StatusDescription;
            diagnosis.ChildId = dto.ChildId;
            diagnosis.EmployeeId = dto.EmployeeId;
            await _uow.DiagnosisRepository.UpdateAsync(diagnosis);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(diagnosis.Id);

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
