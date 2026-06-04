using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.LevelDto.Request;

namespace Qandil.Service.IServices
{
    public interface ILevelService
    {
        public Task<Result<PagedResult<Level>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Level>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(LevelRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(LevelRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
    
}
