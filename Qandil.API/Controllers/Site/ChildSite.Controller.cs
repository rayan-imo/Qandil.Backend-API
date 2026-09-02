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
        [HttpGet("{childId}/pre-test")]
        public async Task<IActionResult> GetChildPostTest(Guid childId)
        {
            var result = await _childSiteService.GetChildPreTestAsync(childId);

            if (!result.IsSuccess)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    MessageAr = result.Error ?? "تعذر جلب معلومات الطفل والاختبار البعدي",
                    MessageEn = "Failed to retrieve child information and post-test"
                });
            }

            return Ok(new ApiResponse<ChildPostTestResponseDto>
            {
                Success = true,
                MessageAr = "تم جلب معلومات الطفل والاختبار البعدي بنجاح",
                MessageEn = "Child information and post-test retrieved successfully",
                Data = result.Value
            });
        }
    }
}
