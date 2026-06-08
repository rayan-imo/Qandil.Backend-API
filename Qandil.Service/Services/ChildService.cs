using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Child;

namespace Qandil.Service.Services
{
    public class ChildService(IUnitOfWork _uow) : IChildService
    {
        public async Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = new BaseSpecification<Child>()                       
            {
                Criteria = x => x.DeletedAt == null
            }.Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Child>>.Success(await _uow.ChildRepository.PagedListAsync(spec));
        }
        public async Task<Result<Child>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Child>.Failure("Child ID cannot be empty.");

            var child = await _uow.ChildRepository.GetByIdAsync(id);

            if (child == null || child.DeletedAt != null)
                return Result<Child>.Failure($"Child with ID was not found.");

            return Result<Child>.Success(child);
        }

        public async Task<Result<Guid>> AddAsync(ChildRequesDto dto)
        {
            await new ChildValidator().ValidateAndThrowAsync(dto);
            var child = new Child
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth,
                MotherName = dto.MotherName,
                FatherName = dto.FatherName,
                Gender = dto.Gender,
                GuardianName = dto.GuardianName,
                GuardianPhoneNumber = dto.GuardianPhoneNumber,
                GuardianRelationship = dto.GuardianRelationship,
                HasDisability = dto.HasDisability,

            };
            await _uow.ChildRepository.AddAsync(child);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(child.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(ChildRequesDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("Child ID cannot be empty.");

            var child = await _uow.ChildRepository.GetByIdAsync(id);

            if (child == null || child.DeletedAt != null)
                return Result<Guid>.Failure($"Child with ID was not found.");

            await new ChildValidator().ValidateAndThrowAsync(dto);

            child.FirstName = dto.FirstName;
            child.LastName = dto.LastName;
            child.Address = dto.Address;
            child.DateOfBirth = dto.DateOfBirth;
            child.MotherName = dto.MotherName;
            child.FatherName = dto.FatherName;
            child.Gender = dto.Gender;
            child.GuardianName = dto.GuardianName;
            child.GuardianPhoneNumber = dto.GuardianPhoneNumber;
            child.GuardianRelationship = dto.GuardianRelationship;
            child.HasDisability = dto.HasDisability;
            await _uow.ChildRepository.UpdateAsync(child);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(child.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Child ID cannot be empty.");

            var child = await _uow.ChildRepository.GetByIdAsync(id);

            if (child == null || child.DeletedAt != null)
                return Result<bool>.Failure($"Child with ID was not found.");

            child.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

    }
}
