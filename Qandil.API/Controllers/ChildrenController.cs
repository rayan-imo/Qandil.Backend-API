using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Children;
using Qandil.API.Dtos.Responses.Children;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.IServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController(IChildService _childService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedResult<ChildResponse>>> GetAll([FromQuery] PaginationParameter paginationParameter)
        {
            var children = await _childService.GetAllAsync(paginationParameter);
            return Ok(children?.Value?.MapTo(c => ChildResponse.Transform(c)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _childService.GetById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "الطفل غير موجود",
                    MessageEn = "Child not found"
                });
            }
            var childEntity = result.Value;

            return Ok(new ApiResponse<ChildResponse>
            {
                Success = true,
                MessageAr = "تم جلب بيانات الطفل بنجاح",
                MessageEn = "Child retrieved successfully",
                Data = ChildResponse.Transform(childEntity),
            });
        }
        [HttpPost]
        public async Task<IActionResult> Add(ChildRequest childRequest)
        {
            var childDto = new ChildRequesDto
            {
                FatherName = childRequest.FatherName,
                LastName = childRequest.LastName,
                MotherName = childRequest.MotherName,
                FirstName = childRequest.FirstName,
                Address = childRequest.Address,
                DateOfBirth = childRequest.DateOfBirth,
                Gender = childRequest.Gender,
                HasDisability = childRequest.HasDisability,

            };
           
            var result = await _childService.AddAsync(childDto);
         
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<ChildResponse>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة الطفل",
                    MessageEn = "Failed to add child"
                });
            }
            var childEntity = result.Value;

            return Ok(new ApiResponse<ChildResponse>
            {
                Success = true,
                MessageAr = "تمت إضافة الطفل بنجاح",
                MessageEn = "Child added successfully",
                Data = ChildResponse.Transform(childEntity),
            });
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(ChildRequest childRequest, Guid id)
        {
            var childDto = new ChildRequesDto
            {
                FatherName = childRequest.FatherName,
                LastName = childRequest.LastName,
                MotherName = childRequest.MotherName,
                FirstName = childRequest.FirstName,
                Address = childRequest.Address,
                DateOfBirth = childRequest.DateOfBirth,
                Gender = childRequest.Gender,
               HasDisability = childRequest.HasDisability,

            };
            var result = await _childService.UpdateAsync(childDto, id);
            var childEntity = result.Value;
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات الطفل",
                    MessageEn = "Failed to update child"

                });
            }

            return Ok(new ApiResponse<ChildResponse>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات الطفل بنجاح",
                MessageEn = "Child updated successfully",
                Data = ChildResponse.Transform(childEntity),
            });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _childService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف الطفل",
                    MessageEn = "Failed to delete child"
                });
            }

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الطفل بنجاح",
                MessageEn = "Child deleted successfully"
            });
        }
    }

}

