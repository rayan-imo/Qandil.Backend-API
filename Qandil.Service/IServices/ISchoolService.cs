using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.SchoolDto.Request;

namespace Qandil.Service.IServices
{
    public interface ISchoolService
    {
        public Task<Result<PagedResult<School>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<School>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(SchoolRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(SchoolRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}

