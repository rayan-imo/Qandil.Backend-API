using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Diagnisis;
using Qandil.API.Dtos.Responses.Diagnosises;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Diagnosis.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosisesConroller(IDiagnosisService _diagnosisService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<DiagnosisResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var diagnosises = await _diagnosisService.GetAllAsync(paginationParameter);
            return Ok(diagnosises?.Value?.MapTo(c => DiagnosisResponse.Transform(c)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
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

            return Ok(new ApiResponse<Diagnosis>
            {
                Success = true,
                MessageAr = "تم جلب بيانات التشخيص بنجاح",
                MessageEn = "Diagnonsise retrieved successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(DiagnosisRequest diagnosisRequest)
        {
            var diagnosisDto = new DiagnosisRequestDto
            {
                DisabilityOnsetDate = diagnosisRequest.DisabilityOnsetDate,
                MedicalNots = diagnosisRequest.MedicalNots,
                StatusDescription = diagnosisRequest.StatusDescription,
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

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                MessageAr = "تمت إضافة التشخيص بنجاح",
                MessageEn = "Diagnonsise added successfully",

                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(DiagnosisRequest diagnosisRequest, Guid id)
        {
            var diagnosisDto = new DiagnosisRequestDto
            {
                DisabilityOnsetDate = diagnosisRequest.DisabilityOnsetDate,
                MedicalNots = diagnosisRequest.MedicalNots,
                StatusDescription = diagnosisRequest.StatusDescription,
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

            return Ok(new ApiResponse<Guid>
            {
                Success = true,

                MessageAr = "تم تحديث بيانات التشخيص بنجاح",
                MessageEn = "Diagnonsise updated successfully",

                Data = result.Value
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
