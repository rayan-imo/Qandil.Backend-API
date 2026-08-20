using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.SubjectDto.Request;
using Qandil.Service.Dtos.TestDto.Requests;
using Qandil.Service.Dtos.TestSubjectDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Level;
using Qandil.Service.Validation.TestSubject;

namespace Qandil.Service.Services
{
    public class TestSubjectSevice(IUnitOfWork _uow):ITestSubjectService
    {
        public async Task<Result<PagedResult<TestSubject>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = BaseSpecification<TestSubject>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);
            return Result<PagedResult<TestSubject>>.Success(await _uow.TestSubjectRepository.PagedListAsync(spec));
        }
        public async Task<Result<TestSubject>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<TestSubject>.Failure("TestSubject ID cannot be empty.");

            var testSubject = await _uow.TestSubjectRepository.GetByIdAsync(id);

            if (testSubject == null || testSubject.DeletedAt != null)
                return Result<TestSubject>.Failure($" TestSubject with ID was not found.");

            return Result<TestSubject>.Success(testSubject);
        }

        public async Task<Result<Guid>> AddAsync(TestSubjectRequestDto dto)
        {
            await new TestSubjectValidator().ValidateAndThrowAsync(dto);
            var testSubject = new TestSubject
            {
               TestId=dto.TestId,
               SubjectId=dto.SubjectId,
               MaxMark=dto.MaxMark,

            };
            await _uow.TestSubjectRepository.AddAsync(testSubject);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(testSubject.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(TestSubjectRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("TestSubject ID cannot be empty.");

            var testSubject = await _uow.TestSubjectRepository.GetByIdAsync(id);

            if (testSubject == null || testSubject.DeletedAt != null)
                return Result<Guid>.Failure($"TestSubject with ID was not found.");

            await new TestSubjectValidator().ValidateAndThrowAsync(dto);
            testSubject.TestId = dto.TestId;
            testSubject.SubjectId = dto.SubjectId;
            testSubject.MaxMark = dto.MaxMark;

            await _uow.TestSubjectRepository.UpdateAsync(testSubject);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(testSubject.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("TestSubject ID cannot be empty.");

            var testSubject = await _uow.TestSubjectRepository.GetByIdAsync(id);

            if (testSubject == null || testSubject.DeletedAt != null)
                return Result<bool>.Failure($"TestSubject with ID was not found.");

            testSubject.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}

