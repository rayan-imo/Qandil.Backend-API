using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Tests;
using Qandil.API.Dtos.Responses.Tests;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.TestDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController(ITestService _testService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<TestResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var tests = await _testService.GetAllAsync(paginationParameter);
            return Ok(tests?.Value?.MapTo(t => TestResponse.Transform(t)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _testService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "الاختبار غير موجود",
                    MessageEn = "Test not found"
                });
            }

            return Ok(new ApiResponse<Test>
            {
                Success = true,
                MessageAr = "تم جلب بيانات الاختبار بنجاح",
                MessageEn = "Test retrieved successfully",
                Data = result.Value
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(TestRequest testRequest)
        {
            var testDto = new TestRequestDto
            {
                
              //  TotalMark=testRequest.TotalMark,
                //SubjectId=testRequest.SubjectId,    
                LevelId=testRequest.LevelId,
            };

            var result = await _testService.AddAsync(testDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة الاختبار",
                    MessageEn = "Failed to add test"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة الاختبار بنجاح",
                MessageEn = "Test added successfully",
                Data = result.Value
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(TestRequest testRequest, Guid id)
        {
            var testDto = new TestRequestDto
            {
              //  TotalMark = testRequest.TotalMark,
                //SubjectId = testRequest.SubjectId,
                LevelId = testRequest.LevelId,
            };

            var result = await _testService.UpdateAsync(testDto, id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات الاختبار",
                    MessageEn = "Failed to update test"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات الاختبار بنجاح",
                MessageEn = "Test updated successfully",
                Data = result.Value
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _testService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف الاختبار",
                    MessageEn = "Failed to delete test"
                });
            }
            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الاختبار بنجاح",
                MessageEn = "Test deleted successfully"
            });
        }
    }
}