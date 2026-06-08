using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Diagnosis.Requests;
using Qandil.Service.Dtos.Disability.Requests;

namespace Qandil.Service.IServices
{
    public interface IDisabilityService
    {
        public Task<Result<PagedResult<Disability>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Disability>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(DisabilityRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(DisabilityRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
