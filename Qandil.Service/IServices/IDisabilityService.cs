using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Diagnosis.Requests;
using Qandil.Service.Dtos.Disability.Requests;

namespace Qandil.Service.IServices
{
    public interface IDisabilityService
    {
        public Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Child>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(DisabilityDto dto);
        public Task<Result<Guid>> UpdateAsync(DiagnosisDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
