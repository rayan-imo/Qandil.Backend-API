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
<<<<<<< HEAD
                    MessageAr = "البرنامج غير موجود",
                    MessageEn = "Program not found"
=======
                    MessageEn = result.Error
>>>>>>> d919681 (Add AuthServices)
                });
            }

            return Ok(new ApiResponse<EduProgram>
            {
                Success = true,
<<<<<<< HEAD
                MessageAr = "تم جلب بيانات البرنامج بنجاح",
                MessageEn = "Program retrieved successfully",
=======
                MessageEn = "EduProgram added successfully",
>>>>>>> d919681 (Add AuthServices)
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
<<<<<<< HEAD
                    MessageAr = "فشلت عملية إضافة البرنامج",
                    MessageEn = "Failed to add program"
=======
                    MessageEn = result.Error
>>>>>>> d919681 (Add AuthServices)
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
<<<<<<< HEAD
                MessageAr = "تمت إضافة البرنامج بنجاح",
                MessageEn = "Program added successfully",
=======
                MessageEn = "EduProgram added successfully",
>>>>>>> d919681 (Add AuthServices)
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
<<<<<<< HEAD
                    MessageAr = "فشل تحديث بيانات البرنامج",
                    MessageEn = "Failed to update program"
=======
                    MessageEn = result.Error
>>>>>>> d919681 (Add AuthServices)

                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
<<<<<<< HEAD
                MessageAr = "تم تحديث بيانات البرنامج",
                MessageEn = "Program updated successfully",
=======
                MessageEn = "EduProgram updated successfully",
>>>>>>> d919681 (Add AuthServices)
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
<<<<<<< HEAD
                    MessageAr = "فشل حذف البرنامج",
                    MessageEn = "Failed to delete program"
=======
                    MessageEn = result.Error
>>>>>>> d919681 (Add AuthServices)
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
<<<<<<< HEAD
                MessageAr = "تم حذف البرنامج بنجاح",
                MessageEn = "Program deleted successfully"
=======
                MessageEn = "EduProgram removed successfully",
>>>>>>> d919681 (Add AuthServices)
            });
        }
    }
}
