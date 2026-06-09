using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<PagedResult<LevelResponse>>> GetAll(
            [FromQuery] PaginationParameter paginationParameter)
        {
            var levels = await _levelService.GetAllAsync(paginationParameter);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                MessageAr = "تم جلب المستويات بنجاح",
                MessageEn = "Levels retrieved successfully",
                Data = levels?.Value?.MapTo(l => LevelResponse.Transform(l))
            });
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
                    MessageAr = "المستوى غير موجود",
                    MessageEn = "Level not found"
                });
            }

            return Ok(new ApiResponse<Level>
            {
                Success = true,
                MessageAr = "تم جلب بيانات المستوى بنجاح",
                MessageEn = "Level retrieved successfully",
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
                    MessageAr = "فشلت عملية إضافة المستوى",
                    MessageEn = "Failed to add level"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة المستوى بنجاح",
                MessageEn = "Level added successfully",
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
                    MessageAr = "فشل تحديث المستوى",
                    MessageEn = "Failed to update level"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث المستوى بنجاح",
                MessageEn = "Level updated successfully",
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
                    MessageAr = "فشل حذف المستوى",
                    MessageEn = "Failed to delete level"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف المستوى بنجاح",
                MessageEn = "Level deleted successfully",
                Data = true
            });
        }
    }
}
