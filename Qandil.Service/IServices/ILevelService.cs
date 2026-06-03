using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos;

namespace Qandil.Service.IServices
{
    public interface ILevelService
    {
        public Task<Result<PagedResult<Level>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Level>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(LevelDto dto);
        public Task<Result<Guid>> UpdateAsync(LevelDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
