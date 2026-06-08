using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.EduProgram;
using Qandil.API.Dtos.Responses.EduPrograms;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Program.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EduProgramsControllers(IEduProgramService _eduProgramService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PagedResult<EduProgramResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var classroom = await _eduProgramService.GetAllAsync(paginationParameter);
            return Ok(classroom?.Value?.MapTo(p => EduProgramResponse.Transform(p)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _eduProgramService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<EduProgram>
            {
                Success = true,
                Message = "EduProgram added successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(EduProgramRequest eduProgramRequest)
        {
            var eduProgramDto = new EduProgramRequestDto
            {
                Name = eduProgramRequest.Name,
                SessionDuration = eduProgramRequest.SessionDuration,
                SessionNumber = eduProgramRequest.SessionNumber,

            };
            var result = await _eduProgramService.AddAsync(eduProgramDto);
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
                Message = "EduProgram added successfully",
                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(EduProgramRequest eduProgramRequest, Guid id)
        {
            var eduProgramDto = new EduProgramRequestDto
            {
                Name = eduProgramRequest.Name,
                SessionDuration = eduProgramRequest.SessionDuration,
                SessionNumber = eduProgramRequest.SessionNumber,

            };
            var result = await _eduProgramService.UpdateAsync(eduProgramDto, id);
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
                Message = "EduProgram updated successfully",
                Data = result.Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _eduProgramService.DeleteAsync(id);
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
                Message = "EduProgram removed successfully",
            });
        }
    }
}
