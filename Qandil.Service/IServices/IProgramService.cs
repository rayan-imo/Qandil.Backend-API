using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Program.Requests;

namespace Qandil.Service.IServices
{
    public interface IProgramService
    {

        public Task<Result<PagedResult<Child>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Child>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(ProgramDto dto);
        public Task<Result<Guid>> UpdateAsync(ProgramDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
