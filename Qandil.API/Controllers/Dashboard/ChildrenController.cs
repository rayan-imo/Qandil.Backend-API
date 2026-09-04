
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.API.Dtos.Requests.Children;
using Qandil.API.Dtos.Responses.Children;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.ChildDto.Request;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildrenController(IChildService _childService) : ControllerBase
    {
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult<PagedResult<ChildAddResponse>>> GetAll(
            [FromQuery] PaginationParameter paginationParameter)
        {
            var children = await _childService.GetAllAsync(paginationParameter);
            return Ok(children?.Value?.MapTo(c => ChildAddResponse.Transform(c)));
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("with-diagnosis")]
        public async Task<ActionResult<PagedResult<ChildAddResponse>>> GetAllWithDiagnosis(
            [FromQuery] PaginationParameter paginationParameter)
        {
            var children = await _childService.GetAllWithDiagnosisAsync(paginationParameter);
            return Ok(children?.Value?.MapTo(c => ChildAddResponse.Transform(c)));
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _childService.GetById(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "الطفل غير موجود",
                    MessageEn = "Child not found"
                });

            return Ok(new ApiResponse<ChildAddResponse>
            {
                Success = true,
                MessageAr = "تم جلب بيانات الطفل بنجاح",
                MessageEn = "Child retrieved successfully",
                Data = ChildAddResponse.Transform(result.Value)
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add(ChildAddRequest childRequest)
        {
            var childDto = new ChildAddRequesDto
            {
                FatherName = childRequest.FatherName,
                LastName = childRequest.LastName,
                MotherName = childRequest.MotherName,
                FirstName = childRequest.FirstName,
                Address = childRequest.Address,
                DateOfBirth = childRequest.DateOfBirth,
                Gender = childRequest.Gender,
                HasDisability = childRequest.HasDisability,
                JoiningDate = childRequest.JoiningDate,
                PlaceOfBearth = childRequest.PlaceOfBearth,
                IsEnrolledInSchool = childRequest.IsEnrolledInSchool,
                SchoolName = childRequest.SchoolName,
                SchoolGrade = childRequest.SchoolGrade,
                FatherJob = childRequest.FatherJob,
                MotherJob = childRequest.MotherJob,
                FamilyMembers = childRequest.FamilyMembers
            };

            var result = await _childService.AddAsync(childDto);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<ChildAddResponse>
                {
                    Success = false,
                    MessageAr = "فشلت عملية إضافة الطفل",
                    MessageEn = "Failed to add child"
                });

            return Ok(new ApiResponse<ChildAddResponse>
            {
                Success = true,
                MessageAr = "تمت إضافة الطفل بنجاح",
                MessageEn = "Child added successfully",
                Data = ChildAddResponse.Transform(result.Value)
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            ChildUpdateRequst childRequest, Guid id)
        {
            var childDto = new ChildUpdateRequesDto
            {
                FatherName = childRequest.FatherName,
                LastName = childRequest.LastName,
                MotherName = childRequest.MotherName,
                FirstName = childRequest.FirstName,
                Address = childRequest.Address,
                DateOfBirth = childRequest.DateOfBirth,
                Gender = childRequest.Gender,
                HasDisability = childRequest.HasDisability,
                JoiningDate = childRequest.JoiningDate,
                PlaceOfBearth = childRequest.PlaceOfBearth,
                IsEnrolledInSchool = childRequest.IsEnrolledInSchool,
                SchoolName = childRequest.SchoolName,
                SchoolGrade = childRequest.SchoolGrade,
                FatherJob = childRequest.FatherJob,
                MotherJob = childRequest.MotherJob,
                FamilyMembers = childRequest.FamilyMembers,
                ProgramId = childRequest.ProgramId,
                ClassroomId = childRequest.ClassroomId
            };

            var result = await _childService.UpdateAsync(childDto, id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    MessageAr = "فشل تحديث بيانات الطفل",
                    MessageEn = "Failed to update child"
                });

            return Ok(new ApiResponse<ChildAddResponse>
            {
                Success = true,
                MessageAr = "تم تحديث بيانات الطفل بنجاح",
                MessageEn = "Child updated successfully",
                Data = ChildAddResponse.Transform(result.Value)
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _childService.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = "فشل حذف الطفل",
                    MessageEn = "Failed to delete child"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                MessageAr = "تم حذف الطفل بنجاح",
                MessageEn = "Child deleted successfully"
            });
        }
    }
}

