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
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<Diagnosis>
            {
                Success = true,
                Message = "Diagnosis added successfully",
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
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "Diagnosis added successfully",
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
                    Message = result.Error

                });
            }

            return Ok(new ApiResponse<Guid>
            {
                Success = true,
                Message = "Diagnosis updated successfully",
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
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Diagnosis removed successfully",
            });
        }

    }
}
