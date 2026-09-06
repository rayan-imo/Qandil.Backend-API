using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.SubjectMarks;
using Qandil.API.Dtos.Responses.SubjectMarks;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.Dtos.SubjectMarkDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectMarkController(ISubjectMarkService _subjectMarkService) : ControllerBase
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<SubjectMarkResponse>>> GetAll(
            [FromQuery] PaginationParameter paginationParameter)
        {
            var subjectMarks = await _subjectMarkService.GetAllAsync(paginationParameter);

            return Ok(subjectMarks?.Value?.MapTo(x => SubjectMarkResponse.Transform(x)));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _subjectMarkService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "علامة المادة غير موجودة",
                    MessageEn = "Subject mark not found"
                });

            return Ok(new ApiResponse<SubjectMarkResponse>
            {
                Success = true,
                MessageAr = "تم جلب علامة المادة بنجاح",
                MessageEn = "Subject mark retrieved successfully",
                Data = SubjectMarkResponse.Transform(result.Value)
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add(SubjectMarkRequest request)
        {
            var subjectMarkDto = new SubjectMarkRequestDto
            {
                ObtainMark = request.ObtainMark,
                ChildTestId = request.ChildTestId,
                SubjectId = request.SubjectId,
                Notes = request.Notes
            };

            var result = await _subjectMarkService.AddAsync(subjectMarkDto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<Guid>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة علامة المادة",
                    MessageEn = "Failed to add subject mark"
                });

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة علامة المادة بنجاح",
                MessageEn = "Subject mark added successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            SubjectMarkRequest request, Guid id)
        {
            var subjectMarkDto = new SubjectMarkRequestDto
            {
                ObtainMark = request.ObtainMark,
                ChildTestId = request.ChildTestId,
                SubjectId = request.SubjectId,
                Notes = request.Notes
            };

            var result = await _subjectMarkService.UpdateAsync(subjectMarkDto, id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<Guid>
                {
                    Success = false,
                    MessageAr = "فشل تحديث علامة المادة",
                    MessageEn = "Failed to update subject mark"
                });

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث علامة المادة بنجاح",
                MessageEn = "Subject mark updated successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _subjectMarkService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    MessageAr = "فشل حذف علامة المادة",
                    MessageEn = "Failed to delete subject mark"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف علامة المادة بنجاح",
                MessageEn = "Subject mark deleted successfully",
                Data = result.Value
            });
        }
    }
}