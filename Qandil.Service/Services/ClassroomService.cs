using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ClassRoom.Requests;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Classroom;

namespace Qandil.Service.Services
{



    public class ClassroomService(IUnitOfWork _uow) : IClassroomService
    {

        public async Task<Result<PagedResult<Classroom>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spac = BaseSpecification<Classroom>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Classroom>>.Success(await _uow.ClassroomRepository.PagedListAsync(spac));

        }


        public async Task<Result<Classroom>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Classroom>.Failure("Classroom ID cannot be empty.");

            var classroom = await _uow.ClassroomRepository.GetByIdAsync(id);

            if (classroom == null || classroom.DeletedAt != null)
                return Result<Classroom>.Failure($"Classroom with ID was not found.");

            return Result<Classroom>.Success(classroom);
        }

        public async Task<Result<Guid>> AddAsync(ClassroomRequestDto dto)
        {
            await new ClassroomValidator().ValidateAndThrowAsync(dto);
            var classroom = new Classroom
            {

                MaxCapacity = dto.MaxCapacity,
                CurrentCapacity = dto.CurrentCapacity,
                ProgramId = dto.ProgramId,
                EmployeeId = dto.EmployeeId,
                LevelId = dto.LevelId,
            };

            await _uow.ClassroomRepository.AddAsync(classroom);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(classroom.Id);


        }

        public async Task<Result<Guid>> UpdateAsync(ClassroomRequestDto dto, Guid id)
        {

            if (id == Guid.Empty)
                return Result<Guid>.Failure("Classroom ID cannot be empty.");

            var classroom = await _uow.ClassroomRepository.GetByIdAsync(id);

            if (classroom == null || classroom.DeletedAt != null)
                return Result<Guid>.Failure($"Classroom with ID was not found.");
            await new ClassroomValidator().ValidateAndThrowAsync(dto);

            classroom.MaxCapacity = dto.MaxCapacity;
            classroom.CurrentCapacity = dto.CurrentCapacity;
            classroom.ProgramId = dto.ProgramId;
            classroom.EmployeeId = dto.EmployeeId;
            classroom.LevelId = dto.LevelId;
            await _uow.ClassroomRepository.UpdateAsync(classroom);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(classroom.Id);

        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Classroom ID cannot be empty.");

            var classroom = await _uow.ClassroomRepository.GetByIdAsync(id);

            if (classroom == null || classroom.DeletedAt != null)
                return Result<bool>.Failure("Classroom with iD was not found ");

            classroom.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();
            return Result<bool>.Success(true);
<<<<<<< HEAD

        }

=======


        }



>>>>>>> d919681 (Add AuthServices)
    }




}



