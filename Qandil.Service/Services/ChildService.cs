using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Child;
using System.Net;
using System.Reflection;

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

        public async Task<Result<Child>> AddAsync(ChildRequesDto dto)
        {
            await new ChildValidator().ValidateAndThrowAsync(dto);
            var child = new Child
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                BirthPlace = dto.BirthPlace,
                MotherName = dto.MotherName,
                FatherName = dto.FatherName,
<<<<<<< HEAD
                Gender = dto.Gender,
=======
                FatherEducationLevel = dto.FatherEducationLevel,
                MotherEducationLevel = dto.MotherEducationLevel,
                TotalFamilyMembers = dto.TotalFamilyMembers,
                ChildOrderAmongSiblings = dto.ChildOrderAmongSiblings,
                GuardianName = dto.GuardianName,
                GuardianPhoneNumber = dto.GuardianPhoneNumber,
                GuardianRelationship = dto.GuardianRelationship,
>>>>>>> d919681 (Add AuthServices)
                HasDisability = dto.HasDisability,
                JoiningDate = dto.JoiningDate,

            };
            await _uow.ChildRepository.AddAsync(child);
            await _uow.CompleteAsync();
            return Result<Child>.Success(child);
        }
        public async Task<Result<Child>> UpdateAsync(ChildRequesDto dto, Guid id)
        {
            var child = await _uow.ChildRepository.GetByIdAsync(id);

            if (child == null || child.DeletedAt != null)
                return Result<Child>.Failure($"Child with ID was not found.");

            await new ChildValidator().ValidateAndThrowAsync(dto);

            child.FirstName = dto.FirstName;
            child.LastName = dto.LastName;
            child.Address = dto.Address;
            child.Gender = dto.Gender;
            child.DateOfBirth = dto.DateOfBirth;
            child.BirthPlace = dto.BirthPlace;
            child.MotherName = dto.MotherName;
            child.FatherName = dto.FatherName;
<<<<<<< HEAD
            child.Gender = dto.Gender;
            child.HasDisability = dto.HasDisability;
            child.JoiningDate = dto.JoiningDate;
=======
            child.FatherEducationLevel = dto.FatherEducationLevel;
            child.MotherEducationLevel = dto.MotherEducationLevel;
            child.TotalFamilyMembers = dto.TotalFamilyMembers;
            child.ChildOrderAmongSiblings = dto.ChildOrderAmongSiblings;
            child.GuardianName = dto.GuardianName;
            child.GuardianPhoneNumber = dto.GuardianPhoneNumber;
            child.GuardianRelationship = dto.GuardianRelationship;
            child.HasDisability = dto.HasDisability;

>>>>>>> d919681 (Add AuthServices)
            await _uow.ChildRepository.UpdateAsync(child);
            await _uow.CompleteAsync();
            return Result<Child>.Success(child);

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
