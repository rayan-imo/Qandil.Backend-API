using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Responses.Children;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildDto;
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
        public async Task<IActionResult> Add(ChildDto dto)
        {
            var result = await _childService.AddAsync(dto);
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
        public async Task<IActionResult> Update(ChildDto dto, Guid id)
        {
            var result = await _childService.UpdateAsync(dto, id);
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

