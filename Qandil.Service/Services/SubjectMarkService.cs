using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.SubjectMarkDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.SubjectMark;

namespace Qandil.Service.Services
{
    public class SubjectMarkService(IUnitOfWork _uow) : ISubjectMarkService
    {
        public async Task<Result<PagedResult<SubjectMark>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = BaseSpecification<SubjectMark>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);
            return Result<PagedResult<SubjectMark>>.Success(await _uow.ChildTestSubjectMarkRepositoy.PagedListAsync(spec));
        }
        public async Task<Result<SubjectMark>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<SubjectMark>.Failure("ChildTestSubjectMark ID cannot be empty.");

            var result = await _uow.ChildTestSubjectMarkRepositoy.GetByIdAsync(id);

            if (result == null || result.DeletedAt != null)
                return Result<SubjectMark>.Failure($" ChildTestSubjectMark with ID was not found.");

            return Result<SubjectMark>.Success(result);
        }

        public async Task<Result<Guid>> AddAsync(SubjectMarkRequestDto dto)
        {
            await new SubjectMarkValidator().ValidateAndThrowAsync(dto);
            var result = new SubjectMark
            {
                ObtainMark = dto.ObtainMark,
                ChildTestId = dto.ChildTestId,
                SubjectId = dto.SubjectId,
                Notes = dto.Notes

            };
            await _uow.ChildTestSubjectMarkRepositoy.AddAsync(result);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(result.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(SubjectMarkRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("ChildTestSubjectMark ID cannot be empty.");

            var result = await _uow.ChildTestSubjectMarkRepositoy.GetByIdAsync(id);

            if (result == null || result.DeletedAt != null)
                return Result<Guid>.Failure($"ChildTestSubjectMark with ID was not found.");

            await new SubjectMarkValidator().ValidateAndThrowAsync(dto);

            result.ObtainMark = dto.ObtainMark;
            result.SubjectId = dto.SubjectId;
            result.Notes = dto.Notes;

            await _uow.ChildTestSubjectMarkRepositoy.UpdateAsync(result);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(result.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("ChildTestSubjectMark ID cannot be empty.");

            var result = await _uow.ChildTestSubjectMarkRepositoy.GetByIdAsync(id);

            if (result == null || result.DeletedAt != null)
                return Result<bool>.Failure($"ChildTestSubjectMark with ID was not found.");

            result.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
