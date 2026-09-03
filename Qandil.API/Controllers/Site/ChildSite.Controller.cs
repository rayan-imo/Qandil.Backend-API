using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qandil.Core.Common;
using Qandil.Service.Dtos.ChildDto.Response;
using Qandil.Service.IServices;

namespace Qandil.API.Controllers.Site
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildSite(IChildSiteService _childSiteService) : ControllerBase
    {
        [Authorize(Roles = "User")]
        [HttpPost("childProfile")]
        public async Task<IActionResult> GetChildPostTest(Guid childId)
        {
            var result = await _childSiteService.GetChildPreTestAsync(childId);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "تعذر جلب معلومات الطفل ",
                    MessageEn = "Failed to retrieve child information "
                });
            }

            return Ok(new ApiResponse<ChildPostTestResponseDto>
            {
                Success = true,
                MessageAr = "تم جلب معلومات الطفل بنجاح",
                MessageEn = "Child information retrieved successfully",
                Data = result.Value
            });
        }
    }
}
