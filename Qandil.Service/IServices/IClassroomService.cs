using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ClassRoom.Requests;

namespace Qandil.Service.IServices
{
    public interface IClassroomService
    {
        public Task<Result<PagedResult<Classroom>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Classroom>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(ClassroomRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(ClassroomRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
