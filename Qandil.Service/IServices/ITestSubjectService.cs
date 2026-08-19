using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.TestSubjectDto.Request;

namespace Qandil.Service.IServices
{
    public interface ITestSubjectService
    {
        public Task<Result<PagedResult<TestSubject>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<TestSubject>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(TestSubjectRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(TestSubjectRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}

