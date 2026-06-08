using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Disability;
using Qandil.API.Dtos.Responses.Diagnosises;
using Qandil.API.Dtos.Responses.Disabilities;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.Disability.Requests;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisabilitiesController(IDisabilityService _disabilityService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<DiagnosisResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var diagnosises = await _disabilityService.GetAllAsync(paginationParameter);
            return Ok(diagnosises?.Value?.MapTo(c => DisabilityResponse.Transform(c)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _disabilityService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Error
                });
            }

            return Ok(new ApiResponse<Disability>
            {
                Success = true,
                Message = "Disabity added successfully",
                Data = result.Value
            });

        }
        [HttpPost]
        public async Task<IActionResult> Add(DisabilityRequest disabilityRequest)
        {
            var disabilityDto = new DisabilityRequestDto
            {
                Name = disabilityRequest.Name,
            };
            var result = await _disabilityService.AddAsync(disabilityDto);
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
                Message = "Disability added successfully",
                Data = result.Value
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(DisabilityRequest disabilityRequest, Guid id)
        {
            var disabilityDto = new DisabilityRequestDto
            {
                Name = disabilityRequest.Name,
            };
            var result = await _disabilityService.UpdateAsync(disabilityDto, id);
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
                Message = "Disability updated successfully",
                Data = result.Value
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _disabilityService.DeleteAsync(id);
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
                Message = "Disability removed successfully",
            });
        }
    }
}
