using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Diagnosis.Requests;

namespace Qandil.Service.IServices
{
    public interface IDiagnosisService
    {
        public Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Child>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(DiagnosisDto dto);
        public Task<Result<Guid>> UpdateAsync(DiagnosisDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
