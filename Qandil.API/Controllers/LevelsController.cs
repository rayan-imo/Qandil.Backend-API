using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Children;
using Qandil.API.Dtos.Requests.Levels;
using Qandil.API.Dtos.Responses.Levels;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.LevelDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController(ILevelService _levelService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<LevelResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var levels = await _levelService.GetAllAsync(paginationParameter);
            return Ok(levels?.Value?.MapTo(l => LevelResponse.Transform(l)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _levelService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<Level>
            {
                Success = true,
                Message = "Level added successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(LevelRequest levelRequest)
        {
            var levelDto = new LevelRequestDto
            {
              LevelName = levelRequest.LevelName,
              ProgramName = levelRequest.ProgramName,
              ProgramId = levelRequest.ProgramId

            };
            var result = await _levelService.AddAsync(levelDto);
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
                Message = "Level added successfully",
                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(LevelRequest levelRequest, Guid id)
        {
            var levelDto = new LevelRequestDto
            {
                LevelName = levelRequest.LevelName,
                ProgramName = levelRequest.ProgramName,
                ProgramId = levelRequest.ProgramId
            };
            var result = await _levelService.UpdateAsync(levelDto, id);
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
                Message = "Level updated successfully",
                Data = result.Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _levelService.DeleteAsync(id);
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
                Message = "Level removed successfully",
            });
        }
    }
}
