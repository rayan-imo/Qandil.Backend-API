using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Schools;
using Qandil.API.Dtos.Responses.Schools;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.SchoolDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController(ISchoolService _schoolService) : ControllerBase
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<SchoolResponse>>> GetAll(
            [FromQuery] PaginationParameter paginationParameter)
        {
            var schools = await _schoolService.GetAllAsync(paginationParameter);
            return Ok(schools?.Value?.MapTo(s => SchoolResponse.Transform(s)));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _schoolService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "المدرسة غير موجودة",
                    MessageEn = "School not found"
                });

            return Ok(new ApiResponse<School>
            {
                Success = true,
                MessageAr = "تم جلب بيانات المدرسة بنجاح",
                MessageEn = "School retrieved successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
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
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة المدرسة",
                    MessageEn = "Failed to add school"
                });

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة المدرسة بنجاح",
                MessageEn = "School added successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            SchoolRequest schoolRequest, Guid id)
        {
            var schoolDto = new SchoolRequestDto
            {
                SchoolName = schoolRequest.SchoolName,
                PhoneNumber = schoolRequest.PhoneNumber,
                PrincipalName = schoolRequest.PrincipalName,
                Address = schoolRequest.Address,
                Notes = schoolRequest.Notes
            };

            var result = await _schoolService.UpdateAsync(schoolDto, id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات المدرسة",
                    MessageEn = "Failed to update school"
                });

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات المدرسة بنجاح",
                MessageEn = "School updated successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _schoolService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف المدرسة",
                    MessageEn = "Failed to delete school"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف المدرسة بنجاح",
                MessageEn = "School deleted successfully"
            });
        }
    }
}

