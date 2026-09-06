using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.TestSubject;
using Qandil.API.Dtos.Responses.TestSubjects;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.Dtos.TestSubjectDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSubjectController(ITestSubjectService _testSubjectService) : ControllerBase
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<TestSubjectResponse>>> GetAll(
            [FromQuery] PaginationParameter paginationParameter)
        {
            var result = await _testSubjectService.GetAllAsync(paginationParameter);

            return Ok(result?.Value?.MapTo(x => TestSubjectResponse.Transform(x)));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _testSubjectService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "المادة غير موجودة ضمن الامتحان",
                    MessageEn = "Test subject not found"
                });

            return Ok(new ApiResponse<TestSubjectResponse>
            {
                Success = true,
                MessageAr = "تم جلب بيانات المادة بنجاح",
                MessageEn = "Test subject retrieved successfully",
                Data = TestSubjectResponse.Transform(result.Value)
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add(TestSubjectRequest request)
        {
            var dto = new TestSubjectRequestDto
            {
                TestId = request.TestId,
                SubjectId = request.SubjectId,
                MaxMark = request.MaxMark
            };

            var result = await _testSubjectService.AddAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<Guid>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة المادة إلى الامتحان",
                    MessageEn = "Failed to add subject to test"
                });

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة المادة إلى الامتحان بنجاح",
                MessageEn = "Subject added to test successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            TestSubjectRequest request, Guid id)
        {
            var dto = new TestSubjectRequestDto
            {
                TestId = request.TestId,
                SubjectId = request.SubjectId,
                MaxMark = request.MaxMark
            };

            var result = await _testSubjectService.UpdateAsync(dto, id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<Guid>
                {
                    Success = false,
                    MessageAr = "فشل تحديث المادة ضمن الامتحان",
                    MessageEn = "Failed to update test subject"
                });

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث المادة ضمن الامتحان بنجاح",
                MessageEn = "Test subject updated successfully",
                Data = result.Value
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _testSubjectService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<bool>
                {
                    Success = false,
                    MessageAr = "فشل حذف المادة من الامتحان",
                    MessageEn = "Failed to remove subject from test"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف المادة من الامتحان بنجاح",
                MessageEn = "Subject removed from test successfully",
                Data = result.Value
            });
        }
    }
}