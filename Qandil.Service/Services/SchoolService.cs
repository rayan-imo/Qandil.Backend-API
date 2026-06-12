using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.SchoolDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.School;

namespace Qandil.Service.Services
{
    public class SchoolService(IUnitOfWork _uow) : ISchoolService
    {
        public async Task<Result<PagedResult<School>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = BaseSpecification<School>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);
            return Result<PagedResult<School>>.Success(await _uow.SchoolRepository.PagedListAsync(spec));
        }
        public async Task<Result<School>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<School>.Failure("School ID cannot be empty.");

            var school = await _uow.SchoolRepository.GetByIdAsync(id);

            if (school == null || school.DeletedAt != null)
                return Result<School>.Failure($" School with ID was not found.");

            return Result<School>.Success(school);
        }

        public async Task<Result<Guid>> AddAsync(SchoolRequestDto dto)
        {
            await new SchoolValidaator().ValidateAndThrowAsync(dto);
            var school = new School
            {
                SchoolName = dto.SchoolName,
                PhoneNumber = dto.PhoneNumber,
                PrincipalName = dto.PrincipalName,
                Address = dto.Address,
                Notes = dto.Notes,
            };
            await _uow.SchoolRepository.AddAsync(school);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(school.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(SchoolRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("School ID cannot be empty.");

            var school = await _uow.SchoolRepository.GetByIdAsync(id);

            if (school == null || school.DeletedAt != null)
                return Result<Guid>.Failure($"Schoo with ID was not found.");

            await new SchoolValidaator().ValidateAndThrowAsync(dto);
            school.SchoolName = dto.SchoolName;
            school.PhoneNumber = dto.PhoneNumber;
            school.PrincipalName = dto.PrincipalName;
            school.Address = dto.Address;
            school.Notes = dto.Notes;

            await _uow.SchoolRepository.UpdateAsync(school);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(school.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("School ID cannot be empty.");

            var school = await _uow.SchoolRepository.GetByIdAsync(id);

            if (school == null || school.DeletedAt != null)
                return Result<bool>.Failure($"School with ID was not found.");

            school.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
