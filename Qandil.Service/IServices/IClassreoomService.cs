using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos;

namespace Qandil.Service.IServices
{
    public interface IClassreoomService
    {
        public Task<Result<PagedResult<Classroom>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Classroom>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(ChildDto dto);
        public Task<Result<Guid>> UpdateAsync(ChildDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
