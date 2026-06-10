using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Classroom;
using Qandil.API.Dtos.Responses.Classrooms;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ClassRoom.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController(IClassroomService _classroomService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<ClassroomResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var classroom = await _classroomService.GetAllAsync(paginationParameter);
            return Ok(classroom?.Value?.MapTo(c => ClassroomResponse.Transform(c)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _classroomService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "الصف غير موجود",
                    MessageEn = "Classroom not found"
                });
            }

            return Ok(new ApiResponse<Classroom>
            {
                Success = true,
                MessageAr = "تم جلب بيانات الصف بنجاح",
                MessageEn = "Classroom retrieved successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(ClassroomRequest classroomRequest)
        {
            var classroomDto = new ClassroomRequestDto
            {
                ClassroomId = classroomRequest.ClassroomId,
                MaxCapacity = classroomRequest.MaxCapacity,
                CurrentCapacity = classroomRequest.CurrentCapacity,
                ProgramId = classroomRequest.ProgramId,
                LevelId = classroomRequest.LevelId,
                EmployeeId = classroomRequest.EmployeeId,

            };
            var result = await _classroomService.AddAsync(classroomDto);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة الصف",
                    MessageEn = "Failed to add Classroom"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة الصف بنجاح",
                MessageEn = "Child added successfully",
                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ClassroomRequest classroomRequest, Guid id)
        {
            var classroomDto = new ClassroomRequestDto
            {
                ClassroomId = classroomRequest.ClassroomId,
                MaxCapacity = classroomRequest.MaxCapacity,
                CurrentCapacity = classroomRequest.CurrentCapacity,
                ProgramId = classroomRequest.ProgramId,
                LevelId = classroomRequest.LevelId,
                EmployeeId = classroomRequest.EmployeeId,

            };
            var result = await _classroomService.UpdateAsync(classroomDto, id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات الصف",
                    MessageEn = "Failed to update classroom"

                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات الصف بنجاح",
                MessageEn = "Classroom updated successfully",
                Data = result.Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _classroomService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف الصف",
                    MessageEn = "Failed to delete classroom"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الصف بنجاح",
                MessageEn = "Classroom deleted successfully"
            });
        }

    }
}
