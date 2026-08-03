using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.TestDto.Requests;

namespace Qandil.Service.IServices
{
    public interface ITestService
    {
        public Task<Result<PagedResult<Test>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Test>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(TestRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(TestRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}

