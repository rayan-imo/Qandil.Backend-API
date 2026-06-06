using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Service.Dtos.ClassRoom.Requests;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Classroom;

namespace Qandil.Service.Services
{
    public class ClassroomService(IUnitOfWork _uow) : IClassroomService
    {
        public async Task<Result<Guid>> AddAsync(ClassroomDto dto)
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

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Classroom ID cannot be empty.")

           /* var child = await _uow.ChildRepository.GetByIdAsync(id);

            if (child == null || child.DeletedAt != null)
                return Result<bool>.Failure($"Child with ID was not found.");

            child.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);*/

        public Task<Result<PagedResult<Classroom>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Classroom>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> UpdateAsync(ClassroomDto dto, Guid id)
        {
            throw new NotImplementedException();
        }
    }


}
