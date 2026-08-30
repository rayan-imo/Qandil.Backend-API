using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildTestSubjectMarkDto.Request;

namespace Qandil.Service.IServices
{
    public interface IChildTestSubjectMarkService
    {
        public Task<Result<PagedResult<SubjectMark>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<SubjectMark>> GetById(Guid id);
       // public Task<Result<Guid>> AddAsync(ChildTestSubjectMarkRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(ChildTestSubjectMarkRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}

