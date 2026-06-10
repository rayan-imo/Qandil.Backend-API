using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildDto.Request;

namespace Qandil.Service.IServices
{
    public interface IChildService
    {
        public Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Child>> GetById(Guid id);
        public Task<Result<Child>> AddAsync(ChildRequesDto dto);
        public Task<Result<Child>> UpdateAsync(ChildRequesDto dto,Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
