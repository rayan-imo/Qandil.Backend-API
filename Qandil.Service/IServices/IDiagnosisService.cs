using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.DiagnosisDto.Requests;

namespace Qandil.Service.IServices
{
    public interface IDiagnosisService
    {
        public Task<Result<PagedResult<Diagnosis>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Diagnosis>> GetById(Guid id);
        public Task<Result<Diagnosis>> AddAsync(DiagnosisRequestDto dto);
        public Task<Result<Diagnosis>> UpdateAsync(DiagnosisRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);



    }
}
