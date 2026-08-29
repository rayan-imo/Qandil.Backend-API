using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.SubjectDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Level;

namespace Qandil.Service.Services
{
    public class SubjectService(IUnitOfWork _uow) : ISubjectService
    {
        public async Task<Result<PagedResult<Subject>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = BaseSpecification<Subject>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);
            return Result<PagedResult<Subject>>.Success(await _uow.SubjectRepository.PagedListAsync(spec));
        }
        public async Task<Result<Subject>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Subject>.Failure("Subject ID cannot be empty.");

            var subject = await _uow.SubjectRepository.GetByIdAsync(id);

            if (subject == null || subject.DeletedAt != null)
                return Result<Subject>.Failure($" Subject with ID was not found.");

            return Result<Subject>.Success(subject);
        }

        public async Task<Result<Guid>> AddAsync(SubjectRequestDto dto)
        {
            await new SubjectValidator().ValidateAndThrowAsync(dto);
            var subject = new Subject
            {
                Name = dto.Name
            };
            await _uow.SubjectRepository.AddAsync(subject);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(subject.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(SubjectRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("Subject ID cannot be empty.");

            var subject = await _uow.SubjectRepository.GetByIdAsync(id);

            if (subject == null || subject.DeletedAt != null)
                return Result<Guid>.Failure($"Subject with ID was not found.");

            await new SubjectValidator().ValidateAndThrowAsync(dto);
            subject.Name = dto.Name;

            await _uow.SubjectRepository.UpdateAsync(subject);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(subject.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Subject ID cannot be empty.");

            var subject = await _uow.SubjectRepository.GetByIdAsync(id);

            if (subject == null || subject.DeletedAt != null)
                return Result<bool>.Failure($"Subject with ID was not found.");

            var childTests = await _uow.ChildTestRepositoy.FindAllAsync
             (x => x.TestId == id && x.DeletedAt != null);

            foreach (var childTest in childTests)
                childTest.DeletedAt = DateTime.UtcNow;

            var childTestSubjectMarks = await _uow.ChildTestSubjectMarkRepositoy.FindAllAsync
                (x => x.SubjectId == id && x.DeletedAt != null);

            foreach (var childTestSubjectMark in childTestSubjectMarks)
                childTestSubjectMark.DeletedAt = DateTime.UtcNow;

            subject.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}

