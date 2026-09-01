using Microsoft.AspNetCore.Authorization;
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
    public class EduProgramsController(IEduProgramService _eduProgramService) : ControllerBase
    {

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<EduProgramResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var classroom = await _eduProgramService.GetAllAsync(paginationParameter);
            return Ok(classroom?.Value?.MapTo(p => EduProgramResponse.Transform(p)));
        }

        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _eduProgramService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "البرنامج غير موجود",
                    MessageEn = "Program not found"
                });
            }

            return Ok(new ApiResponse<EduProgram>
            {
                Success = true,
                MessageAr = "تم جلب بيانات البرنامج بنجاح",
                MessageEn = "Program retrieved successfully",

                Data = result.Value
            });

        }
        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
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
                    MessageAr = "فشلت عملية إضافة البرنامج",
                    MessageEn = "Failed to add program"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة البرنامج بنجاح",
                MessageEn = "Program added successfully",

                Data = result.Value
            });
        }
        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
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
                    MessageAr = "فشل تحديث بيانات البرنامج",
                    MessageEn = "Failed to update program"


                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات البرنامج",
                MessageEn = "Program updated successfully",
                Data = result.Value
            });
        }
        [Authorize(Roles = "Admin,SuperAdmin,Teacher,Specialist")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _eduProgramService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف البرنامج",
                    MessageEn = "Failed to delete program"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
               MessageAr = "تم حذف البرنامج بنجاح",
               MessageEn = "Program deleted successfully"
              
            });
        }
    }
}
