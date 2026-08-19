using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.SubjectDto.Request;

namespace Qandil.Service.IServices
{
    public interface ISubjectService
    {
        public Task<Result<PagedResult<Subject>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Subject>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(SubjectRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(SubjectRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}

