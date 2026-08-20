using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Child;

namespace Qandil.Service.Services
{
    public class ChildService(IUnitOfWork _uow) : IChildService
    {
        public async Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = BaseSpecification<Child>
                  .Create()
                  .Where(x => x.DeletedAt == null)
                  .Paginate(paginationParameter.page, paginationParameter.pageSize);

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

        public async Task<Result<Child>> AddAsync(ChildAddRequesDto dto)
        {
            await new ChildAddValidator().ValidateAndThrowAsync(dto);
            var child = new Child
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                MotherName = dto.MotherName,
                FatherName = dto.FatherName,
                HasDisability = dto.HasDisability,
                JoiningDate = dto.JoiningDate,
                PlaceOfBearth = dto.PlaceOfBearth,
                IsEnrolledInSchool = dto.IsEnrolledInSchool,
                SchoolName = dto.SchoolName,
                SchoolGrade = dto.SchoolGrade,
                FatherJob = dto.FatherJob,
                MotherJob = dto.MotherJob,
                FamilyMembers = dto.FamilyMembers,

            };
            await _uow.ChildRepository.AddAsync(child);
            await _uow.CompleteAsync();
            return Result<Child>.Success(child);
        }
        public async Task<Result<Child>> UpdateAsync(ChildUpdateRequesDto dto, Guid id)
        {
            var child = await _uow.ChildRepository.GetByIdAsync(id);

            if (child == null || child.DeletedAt != null)
                return Result<Child>.Failure($"Child with ID was not found.");

            await new ChildUpdateValidator().ValidateAndThrowAsync(dto);

            child.FirstName = dto.FirstName;
            child.LastName = dto.LastName;
            child.Address = dto.Address;
            child.Gender = dto.Gender;
            child.DateOfBirth = dto.DateOfBirth;
            child.MotherName = dto.MotherName;
            child.FatherName = dto.FatherName;
            child.HasDisability = dto.HasDisability;
            child.JoiningDate = dto.JoiningDate;
            child.PlaceOfBearth = dto.PlaceOfBearth;
            child.IsEnrolledInSchool = dto.IsEnrolledInSchool;
            child.SchoolName = dto.SchoolName;
            child.SchoolGrade = dto.SchoolGrade;
            child.FatherJob = dto.FatherJob;
            child.MotherJob = dto.MotherJob;
            child.FamilyMembers = dto.FamilyMembers;
            child.ProgramId = dto.ProgramId;
            child.ClassroomId = dto.ClassroomId;


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

            var childTests = await _uow.ChildTestRepositoy.FindAllAsync
               (x => x.ChildId == id && x.DeletedAt != null);

            foreach (var childTest in childTests)
                childTest.DeletedAt = DateTime.UtcNow;

            child.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

    }
}
