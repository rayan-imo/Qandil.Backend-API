using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.LevelDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Level;
namespace Qandil.Service.Services
{
    public class LevelService(IUnitOfWork _uow) : ILevelService
    {
        public async Task<Result<PagedResult<Level>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = new BaseSpecification<Level>()
            {
                Criteria = x => x.DeletedAt == null
            }.Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Level>>.Success(await _uow.LevelRepository.PagedListAsync(spec));
        }
        public async Task<Result<Level>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Level>.Failure("Level ID cannot be empty.");

            var level = await _uow.LevelRepository.GetByIdAsync(id);

            if (level == null || level.DeletedAt != null)
                return Result<Level>.Failure($" Level with ID was not found.");

            return Result<Level>.Success(level);
        }

        public async Task<Result<Guid>> AddAsync(LevelRequestDto dto)
        {
            await new LevelValidator().ValidateAndThrowAsync(dto);
            var level = new Level
            {
                LevelName = dto.LevelName,
                ProgramId = dto.ProgramId


            };
            await _uow.LevelRepository.AddAsync(level);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(level.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(LevelRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("Level ID cannot be empty.");

            var level = await _uow.LevelRepository.GetByIdAsync(id);

            if (level == null || level.DeletedAt != null)
                return Result<Guid>.Failure($"Level with ID was not found.");

            await new LevelValidator().ValidateAndThrowAsync(dto);


            level.LevelName = dto.LevelName;
            level.ProgramId = dto.ProgramId;

            await _uow.LevelRepository.UpdateAsync(level);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(level.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Level ID cannot be empty.");

            var level = await _uow.LevelRepository.GetByIdAsync(id);

            if (level == null || level.DeletedAt != null)
                return Result<bool>.Failure($"Level with ID was not found.");

            level.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

    }
}
