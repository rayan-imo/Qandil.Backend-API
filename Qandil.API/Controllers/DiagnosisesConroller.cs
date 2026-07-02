using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Diagnisis;
using Qandil.API.Dtos.Responses.Diagnosises;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Service.Dtos.DiagnosisDto.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisesConroller(IDiagnosisService _diagnosisService, IAnswerService answerService) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PagedResult<DiagnosisResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var diagnosises = await _diagnosisService.GetAllAsync(paginationParameter);
            return Ok(diagnosises?.Value?.MapTo(c => DiagnosisResponse.Transform(c)));
        }

        [HttpGet("{diagnosisId}/full")]
        public async Task<ActionResult<ApiResponse<FullDiagnosisResponse>>> GetFullDiagnosis(Guid id)
        {
            var result = await _diagnosisService.GetFullDiagnosisAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "التشخيص غير موجود",
                    MessageEn = "Diagnnsise not found"
                });

            }

            var resultValu = result.Value;
            var fullDiaignosis = new FullDiagnosisResponse
            {
                ChildId = resultValu.ChildId,
                DiagnosisId = resultValu.DiagnosisId,
                DiagnosisQuestions = resultValu.DiagnosisQuestions,
                Evaluations = resultValu.Evaluations,

            };
            return Ok(new ApiResponse<FullDiagnosisResponse>
            {
                Success = true,
                MessageAr = "تم جلب بيانات التشخيص بنجاح",
                MessageEn = "Diagnonsise retrieved successfully",
                Data = fullDiaignosis

            });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<DiagnosisResponse>>> GetById(Guid id)
        {
            var result = await _diagnosisService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "التشخيص غير موجود",
                    MessageEn = "Diagnnsise not found"
                });
            }
            var diagnosisEntity = result.Value;

            return Ok(new ApiResponse<DiagnosisResponse>
            {
                Success = true,
                MessageAr = "تم جلب بيانات التشخيص بنجاح",
                MessageEn = "Diagnonsise retrieved successfully",
                Data = DiagnosisResponse.Transform(diagnosisEntity)
            });

        }
        [HttpPost]
        public async Task<ActionResult<DiagnosisResponse>> Add(DiagnosisRequest diagnosisRequest)
        {
            var diagnosisDto = new DiagnosisRequestDto
            {
                DisabilityOnsetDate = diagnosisRequest.DisabilityOnsetDate,
                MedicalNots = diagnosisRequest.MedicalNots,
                EmployeeId = diagnosisRequest.EmployeeId,
                ChildId = diagnosisRequest.ChildId,
            };
            var result = await _diagnosisService.AddAsync(diagnosisDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة التشخيص",
                    MessageEn = "Failed to add Diagnonsise"

                });
            }
            var diagnosisEntity = result.Value;

            return Ok(new ApiResponse<DiagnosisResponse>
            {
                Success = true,
                MessageAr = "تمت إضافة التشخيص بنجاح",
                MessageEn = "Diagnonsise added successfully",

                Data = DiagnosisResponse.Transform(diagnosisEntity)
            });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<DiagnosisResponse>> Update(DiagnosisRequest diagnosisRequest, Guid id)
        {
            var diagnosisDto = new DiagnosisRequestDto
            {
                DisabilityOnsetDate = diagnosisRequest.DisabilityOnsetDate,
                MedicalNots = diagnosisRequest.MedicalNots,
                EmployeeId = diagnosisRequest.EmployeeId,
                ChildId = diagnosisRequest.ChildId,
            };
            var result = await _diagnosisService.UpdateAsync(diagnosisDto, id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات التشخيص",
                    MessageEn = "Failed to update diagnonsise"

                });
            }
            var diagnosisEntity = result.Value;
            return Ok(new ApiResponse<DiagnosisResponse>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات التشخيص بنجاح",
                MessageEn = "Diagnonsise updated successfully",
                Data = DiagnosisResponse.Transform(diagnosisEntity)

            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _diagnosisService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف التشخيص",
                    MessageEn = "Failed to delete Diagnosise"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف التشخيص بنجاح",
                MessageEn = "Diagnosise deleted successfully"


            });

        }
    }
}

