using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos;

namespace Qandil.Service.IServices
{
    public interface IEmployeeService
    {
        public Task<Result<PagedResult<Employee>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Employee>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(EmployeeDto dto);
        public Task<Result<Guid>> UpdateAsync(EmployeeDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
