using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Program.Requests;

namespace Qandil.Service.IServices
{
    public interface IEduProgramService
    {

        public Task<Result<PagedResult<EduProgram>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<EduProgram>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(EduProgramRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(EduProgramRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
