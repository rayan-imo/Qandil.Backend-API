using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Children;
using Qandil.API.Dtos.Responses.Children;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController(IChildService _childService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<ChildResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var children = await _childService.GetAllAsync(paginationParameter);
            return Ok(children?.Value?.MapTo(c => ChildResponse.Transform(c)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _childService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<Child>
            {
                Success = true,
                Message = "Child added successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(ChildRequest childRequest)
        {
            var childDto = new ChildRequesDto
            {
                FatherName = childRequest.FatherName,
                LastName = childRequest.LastName,
                MotherName = childRequest.MotherName,
                FirstName = childRequest.FirstName,
                Address = childRequest.Address,
                DateOfBirth = childRequest.DateOfBirth,
                Gender = childRequest.Gender,
                GuardianName = childRequest.GuardianName,
                GuardianRelationship = childRequest.GuardianRelationship,
                GuardianPhoneNumber = childRequest.GuardianPhoneNumber,
                HasDisability = childRequest.HasDisability,

            };
            var result = await _childService.AddAsync(childDto);
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
                Message = "Child added successfully",
                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ChildRequest childRequest, Guid id)
        {
            var childDto = new ChildRequesDto
            {
                FatherName = childRequest.FatherName,
                LastName = childRequest.LastName,
                MotherName = childRequest.MotherName,
                FirstName = childRequest.FirstName,
                Address = childRequest.Address,
                DateOfBirth = childRequest.DateOfBirth,
                Gender = childRequest.Gender,
                GuardianName = childRequest.GuardianName,
                GuardianRelationship = childRequest.GuardianRelationship,
                GuardianPhoneNumber = childRequest.GuardianPhoneNumber,
                HasDisability = childRequest.HasDisability,

            };
            var result = await _childService.UpdateAsync(childDto, id);
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
                Message = "Child updated successfully",
                Data = result.Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _childService.DeleteAsync(id);
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
                Message = "Child removed successfully",
            });
        }
    }

}

