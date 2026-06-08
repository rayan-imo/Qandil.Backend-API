using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;

namespace Qandil.Service.IServices
{
    public interface IChildService
    {
        public Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Child>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(ChildRequesDto dto);
        public Task<Result<Guid>> UpdateAsync(ChildRequesDto dto,Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
