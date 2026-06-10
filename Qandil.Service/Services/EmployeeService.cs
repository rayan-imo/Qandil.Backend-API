using FluentValidation;
using FluentValidation.Validators;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.Employee.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Child;
using Qandil.Service.Validation.Employee;

namespace Qandil.Service.Services
{
    public class EmployeeService(IUnitOfWork _uow) : IEmployeeService
    {
        public async Task<Result<PagedResult<Employee>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = new BaseSpecification<Employee>()
            {
                Criteria = x => x.DeletedAt == null
            }.Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Employee>>.Success(await _uow.EmployeeRepository.PagedListAsync(spec));
        }
        public async Task<Result<Employee>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Employee>.Failure("Employee ID cannot be empty.");

            var employee = await _uow.EmployeeRepository.GetByIdAsync(id);

            if (employee == null || employee.DeletedAt != null)
                return Result<Employee>.Failure($"Employee with ID was not found.");

            return Result<Employee>.Success(employee);
        }

        public async Task<Result<Guid>> AddAsync(EmployeeRequestDto dto)
        {
            await new EmployeeValidator().ValidateAndThrowAsync(dto);
            var employee = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age,
                Specicality = dto.Specicality,
                Email = dto.Email,

            };
            await _uow.EmployeeRepository.AddAsync(employee);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(employee.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(EmployeeRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("Employee ID cannot be empty.");

            var employee = await _uow.EmployeeRepository.GetByIdAsync(id);

            if (employee == null || employee.DeletedAt != null)
                return Result<Guid>.Failure($"Employee with ID was not found.");

            await new EmployeeValidator().ValidateAndThrowAsync(dto);

            employee.FirstName = dto.FirstName;
            employee.LastName = dto.LastName;
            employee.Age = dto.Age;
            employee.Specicality = dto.Specicality;

            await _uow.EmployeeRepository.UpdateAsync(employee);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(employee.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Child ID cannot be empty.");

            var employee = await _uow.EmployeeRepository.GetByIdAsync(id);

            if (employee == null || employee.DeletedAt != null)
                return Result<bool>.Failure($"Child with ID was not found.");

            employee.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

    }
}
