using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Employee.Request;

namespace Qandil.Service.IServices
{
    public interface IEmployeeService
    {
        public Task<Result<PagedResult<Employee>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<Employee>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(EmployeeRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(EmployeeRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
    }
}
