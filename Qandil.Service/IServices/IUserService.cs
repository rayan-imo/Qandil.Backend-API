using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.UserDto.Request;

namespace Qandil.Service.IServices
{
    public interface IUserService
    {
        public Task<Result<PagedResult<User>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<User>> GetById(Guid id);
        public Task<Result<User>> AddAsync(UserRequestdto dto);
        public Task<Result<User>> UpdateAsync(UserRequestdto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}

