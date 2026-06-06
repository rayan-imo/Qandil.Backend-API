using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Children;
using Qandil.API.Dtos.Requests.Schools;
using Qandil.API.Dtos.Responses.Children;
using Qandil.API.Dtos.Responses.Schools;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.Dtos.SchoolDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController(ISchoolService _schoolService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<SchoolResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var schools = await _schoolService.GetAllAsync(paginationParameter);
            return Ok(schools?.Value?.MapTo(s => SchoolResponse.Transform(s)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _schoolService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<School>
            {
                Success = true,
                Message = "School added successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(SchoolRequest schoolRequest)
        {
            var schoolDto = new SchoolRequestDto
            {
                SchoolName = schoolRequest.SchoolName,
                PhoneNumber = schoolRequest.PhoneNumber,
                PrincipalName = schoolRequest.PrincipalName,
                Address = schoolRequest.Address,
                Notes = schoolRequest.Notes
            };
            var result = await _schoolService.AddAsync(schoolDto);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "School added successfully",
                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(SchoolRequest schoolRequest, Guid id)
        {
            var schoolDto = new SchoolRequestDto
            {
                SchoolName = schoolRequest.SchoolName,
                PhoneNumber = schoolRequest.PhoneNumber,
                PrincipalName = schoolRequest.PrincipalName,
                Address = schoolRequest.Address,
                Notes = schoolRequest.Notes
            };
            var result = await _schoolService.UpdateAsync(schoolDto,id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Error

                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "School updated successfully",
                Data = result.Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _schoolService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "School removed successfully",
            });
        }
    }
}
