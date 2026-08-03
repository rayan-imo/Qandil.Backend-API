using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Employees;
using Qandil.API.Dtos.Responses.Employees;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.EmployeeDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService _employeeService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<EmployeeResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var employees = await _employeeService.GetAllAsync(paginationParameter);
            return Ok(employees?.Value?.MapTo(e => EmployeeResponse.Transform(e)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _employeeService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "الموظف غير موجود",
                    MessageEn = "Employee not found"
                });
            }

            return Ok(new ApiResponse<Employee>
            {
                Success = true,
                MessageAr = "تم جلب بيانات الموظف بنجاح",
                MessageEn = "Employee retrieved successfully",
                Data = result.Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeRequest employeeRequest)
        {
            var employeeDto = new EmployeeRequestDto
            {
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                Age = employeeRequest.Age,
                Email = employeeRequest.Email,
                Specicality = employeeRequest.Specicality,
            };

            var result = await _employeeService.AddAsync(employeeDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة الموظف",
                    MessageEn = "Failed to add employee"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة الموظف بنجاح",
                MessageEn = "Employee added successfully",
                Data = result.Value
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EmployeeRequest employeeRequest, Guid id)
        {
            var employeeDto = new EmployeeRequestDto
            {
                FirstName = employeeRequest.FirstName,
                LastName = employeeRequest.LastName,
                Age = employeeRequest.Age,
                Email = employeeRequest.Email,
                Specicality = employeeRequest.Specicality,
            };

            var result = await _employeeService.UpdateAsync(employeeDto, id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات الموظف",
                    MessageEn = "Failed to update employee"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات الموظف بنجاح",
                MessageEn = "Employee updated successfully",
                Data = result.Value
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _employeeService.DeleteAsync(id); if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف الموظف",
                    MessageEn = "Failed to delete employee"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الموظف بنجاح",
                MessageEn = "Employee deleted successfully"
            });
        }
    }
}
