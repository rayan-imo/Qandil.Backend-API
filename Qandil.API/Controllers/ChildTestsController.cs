using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.ChildTests;
using Qandil.API.Dtos.Responses.ChildTests;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.Dtos.ChildTestDto.Requests;
using Qandil.Service.Dtos.ChildTestSubjectMarkDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildTestsController(IChildTestService _childTestService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var result = await _childTestService.GetAllAsync(paginationParameter);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل جلب اختبارات الأطفال ",
                    MessageEn = "Failed to retrieve cheldren tests"
                });
            }

            return Ok(new ApiResponse<PagedResult<ChildTestResponse>>
            {
                Success = true,
                MessageAr = "تم جلب اختبارات الأطفال بنجاح",
                MessageEn = "Child tests retrieved successfully",
                Data = result.Value.MapTo(ct => ChildTestResponse.Transform(ct))
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _childTestService.GetById(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "الاختبار غير موجود",
                    MessageEn = "Child test not found"
                });
            }

            return Ok(new ApiResponse<ChildTestResponse>
            {
                Success = true,
                MessageAr = "تم جلب الاختبار بنجاح",
                MessageEn = "Child test retrieved successfully",
                Data = ChildTestResponse.Transform(result.Value)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChildTestAddRequest childTestRequest)
        {
            var childTestDto = new ChildTestAddRequestDto
            {
                ChildId = childTestRequest.ChildId,
                TestId = childTestRequest.TestId,
                EmployeeId = childTestRequest.EmployeeId,
                Type = childTestRequest.Type,
                Nots = childTestRequest.Nots,
                SubjectMarkDtos = childTestRequest.SubjectMarks.Select(m => new ChildTestSubjectMarkRequestDto
                {
                    ObtainMark = m.ObtainMark,
                    Notes = m.Notes
                }).ToList()
            };

            var result = await _childTestService.AddAsync(childTestDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشلت عملية إضافة الاختبار",
                    MessageEn = "Failed to add child test"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة الاختبار بنجاح",
                MessageEn = "Child test added successfully",
                Data = result.Value
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ChildTestUpdateRequest childTestRequest, Guid id)
        {
            var childTestDto = new ChildTestUpdateRequestDto
            {
                ChildId = childTestRequest.ChildId,
                TestId = childTestRequest.TestId,
                EmployeeId = childTestRequest.EmployeeId,
                Type = childTestRequest.Type,
                Nots = childTestRequest.Nots
            };

            var result = await _childTestService.UpdateAsync(childTestDto, id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "فشل تحديث معلومات اختبار الطفل ",
                    MessageEn = "Failed to update child test information"
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تم تحديث معلومات اختبار الطفل بنجاح",
                MessageEn = "Child test information updated successfully",
                Data = result.Value
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _childTestService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف الاختبار",
                    MessageEn = "Failed to delete child test"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الاختبار بنجاح",
                MessageEn = "Child test deleted successfully"
            });
        }


        [HttpGet("childTest/{childId}/history")]
        public async Task<IActionResult> GetChildExamLevelHistory(Guid childId)
        {
            var result = await _childTestService.GetChildExamHistoryAsync(childId);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "تعذر جلب سجل امتحانات الطفل",
                    MessageEn = "Failed to retrieve child's exam history"
                });
            }

            // تحويل من Service DTO إلى API DTO
            var serviceData = result.Value;
            var apiResponse = new ChildExamHistoryResponse
            {
                ChildFullName = serviceData.ChildFullName,
                Levels = serviceData.Levels.Select(level => new LevelExamsApiResponse
                {
                    LevelName = level.LevelName,
                    Attempts = level.Attempts.Select(attempt => new AttemptApiResponse
                    {
                        AttemptNumber = attempt.AttemptNumber,
                        PreTest = attempt.PreTest != null ? new TestDetailApiResponse
                        {
                            Date = attempt.PreTest.Date,
                            Result = attempt.PreTest.Result,
                            IsPassed = attempt.PreTest.IsPassed,
                            Notes = attempt.PreTest.Notes,
                            EmployeeName = attempt.PreTest.EmployeeName
                        } : null,
                        PostTest = attempt.PostTest != null ? new TestDetailApiResponse
                        {
                            Date = attempt.PostTest.Date,
                            Result = attempt.PostTest.Result,
                            IsPassed = attempt.PostTest.IsPassed,
                            Notes = attempt.PostTest.Notes,
                            EmployeeName = attempt.PostTest.EmployeeName
                        } : null
                    }).ToList()
                }).ToList()
            };

            return Ok(new ApiResponse<ChildExamHistoryResponse>
            {
                Success = true,
                MessageAr = "تم جلب سجل الامتحانات بنجاح",
                MessageEn = "Exam history retrieved successfully",
                Data = apiResponse
            });
        }




        [HttpGet("child/{childId}/test/{testId}/attempts")]
        public async Task<IActionResult> GetChildTestAttempts(Guid childId, Guid testId)
        {
            var result = await _childTestService.GetChildTestAttemptsAsync(childId, testId);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "تعذر جلب محاولات الطفل لهذا الامتحان",
                    MessageEn = "Failed to retrieve child's test attempts"
                });
            }

            // تحويل من Service DTO إلى API DTO
            var serviceData = result.Value;
            var apiResponse = new ChildTestAttemptsResponse
            {
                TestName = serviceData.TestName,
                LevelName = serviceData.LevelName,
                Attempts = serviceData.Attempts.Select(attempt => new AttemptWithMarksApiResponse
                {
                    AttemptNumber = attempt.AttemptNumber,
                    PreTest = attempt.PreTest != null ? new TestWithMarksApiResponse
                    {
                        ChildTestId = attempt.PreTest.ChildTestId,
                        Date = attempt.PreTest.Date,
                        Result = attempt.PreTest.Result,
                        IsPassed = attempt.PreTest.IsPassed,
                        Notes = attempt.PreTest.Notes,
                        EmployeeName = attempt.PreTest.EmployeeName,
                        SubjectMarks = attempt.PreTest.SubjectMarks.Select(m => new SubjectMarkApiResponse
                        {
                            SubjectName = m.SubjectName,
                            Mark = m.Mark
                        }).ToList()
                    } : null,
                    PostTest = attempt.PostTest != null ? new TestWithMarksApiResponse
                    {
                        ChildTestId = attempt.PostTest.ChildTestId,
                        Date = attempt.PostTest.Date,
                        Result = attempt.PostTest.Result,
                        IsPassed = attempt.PostTest.IsPassed,
                        Notes = attempt.PostTest.Notes,
                        EmployeeName = attempt.PostTest.EmployeeName,
                        SubjectMarks = attempt.PostTest.SubjectMarks.Select(m => new SubjectMarkApiResponse
                        {
                            SubjectName = m.SubjectName,
                            Mark = m.Mark
                        }).ToList()
                    } : null
                }).ToList()
            };

            return Ok(new ApiResponse<ChildTestAttemptsResponse>
            {
                Success = true,
                MessageAr = "تم جلب المحاولات بنجاح",
                MessageEn = "Test attempts retrieved successfully",
                Data = apiResponse
            });
        }
    }
}

   
