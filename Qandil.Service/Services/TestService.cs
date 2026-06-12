using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.TestDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Test;

namespace Qandil.Service.Services
{
    public class TestService(IUnitOfWork _uow) : ITestService
    {
        public async Task<Result<PagedResult<Test>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spac = BaseSpecification<Test>
                 .Create()
                 .Where(x => x.DeletedAt == null)
                 .Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Test>>.Success(await _uow.TestRepository.PagedListAsync(spec));
        }
        public async Task<Result<Test>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Test>.Failure("Test ID cannot be empty.");

            var test = await _uow.TestRepository.GetByIdAsync(id);

            if (test == null || test.DeletedAt != null)
                return Result<Test>.Failure($" Test with ID was not found.");

            return Result<Test>.Success(test);
        }

        public async Task<Result<Guid>> AddAsync(TestRequestDto dto)
        {
            await new TestValidator().ValidateAndThrowAsync(dto);
            var test = new Test
            {
                TestName = dto.TestName,
                TestDate = dto.TestDate,
                Description = dto.Description,
                TestType = dto.TestType
            };
            await _uow.TestRepository.AddAsync(test);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(test.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(TestRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("Test ID cannot be empty.");

            var test = await _uow.TestRepository.GetByIdAsync(id);

            if (test == null || test.DeletedAt != null)
                return Result<Guid>.Failure($"Test with ID was not found.");

            await new TestValidator().ValidateAndThrowAsync(dto);

            test.TestName = dto.TestName;
            test.TestDate = dto.TestDate;
            test.Description = dto.Description;
            test.TestType = dto.TestType;

            await _uow.TestRepository.UpdateAsync(test);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(test.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Test ID cannot be empty.");

            var test = await _uow.TestRepository.GetByIdAsync(id);

            if (test == null || test.DeletedAt != null)
                return Result<bool>.Failure($"Test with ID was not found.");

            test.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}
