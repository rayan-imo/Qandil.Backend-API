using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildTestDto.Requests;

namespace Qandil.Service.IServices
{
    public interface IChildTestService
    {
        public Task<Result<PagedResult<ChildTest>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<ChildTest>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(ChildTestRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(ChildTestRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
        public Task<Result<IEnumerable<ChildLevelAverageDto>>> GetChildAveragesByChildIdAsync(Guid childId);
        public Task<Result<ChildTestsDto>> GetChildLevelAveragesAsync(Guid childId, Guid levelId);
    }
}

