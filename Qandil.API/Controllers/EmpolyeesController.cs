using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Responses.Employees;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpolyeesController(IEmployeeService _employeeService) : ControllerBase
    {
       [HttpGet]
       public async Task<ActionResult<PagedResult<EmployeeResponse>>>GetAll([FromQuery] PaginationParameter paginationParameter)
       {
           var employees = await _employeeService.GetAllAsync(paginationParameter);
           return Ok(employees?.Value?.MapTo(e=>EmployeeResponse.Transform(e)));
       }
    }
}
