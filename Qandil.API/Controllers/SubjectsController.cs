using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Subject;
using Qandil.API.Dtos.Responses.Subjects;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.Dtos.SubjectDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(ISubjectService _subjectService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PagedResult<SubjectResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var result = await _subjectService.GetAllAsync(paginationParameter);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل في جلب المواد الدراسية",
                    MessageEn = "Failed to retrieve subjects"
                });
            }

            var subjects = result.Value;
            return Ok(new ApiResponse<PagedResult<SubjectResponse>>
            {
                Success = true,
                MessageAr = "تم جلب المواد الدراسية بنجاح",
                MessageEn = "Subjects retrieved successfully",
                Data = subjects.MapTo(s => SubjectResponse.Transform(s))
            });


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _subjectService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "المادة الدراسية غير موجودة",
                    MessageEn = "Subject not found"
                });
            }

            var subjectEntity = result.Value;

            return Ok(new ApiResponse<SubjectResponse>
            {
                Success = true,
                MessageAr = "تم جلب المادة الدراسية بنجاح",
                MessageEn = "Subject retrieved successfully",
                Data = SubjectResponse.Transform(subjectEntity),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubjectRequest subjectRequest)
        {
            var subjectDto = new SubjectRequestDto
            {
                Name = subjectRequest.Name
            };

            var result = await _subjectService.AddAsync(subjectDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<Guid>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة المادة الدراسية",
                    MessageEn = "Failed to add subject"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة المادة الدراسية بنجاح",
                MessageEn = "Subject added successfully",
                Data = result.Value,
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(SubjectRequest subjectRequest, Guid id)
        {
            var subjectDto = new SubjectRequestDto
            {
                Name = subjectRequest.Name
            };

            var result = await _subjectService.UpdateAsync(subjectDto, id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات المادة الدراسية",
                    MessageEn = "Failed to update subject"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات المادة الدراسية بنجاح",
                MessageEn = "Subject updated successfully",
                Data = result.Value,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _subjectService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف المادة الدراسية",
                    MessageEn = "Failed to delete subject"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف المادة الدراسية بنجاح",
                MessageEn = "Subject deleted successfully",
                Data = result.Value
            });
        }
    }
}

 