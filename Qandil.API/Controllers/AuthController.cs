using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request;
using Qandil.Service.AuthServices.Helper.Dtos.Requests;
using Qandil.Service.AuthServices.Helper.Dtos.Responses;
using Qandil.Services.AuthServices.Helper;
using Qandil.Services.AuthServices.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Qandil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogInAsync(
            LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LogInAsync(model);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

      

        [Authorize(Policy = "CreateAdminPolicy")]
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(
            CreateAdminDto dto)
        {
            var result = await _authService.CreateAdminAsync(dto);

            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }

            return Ok(new CreateUserResponseDto
            {
                Message = result.Message,
                Email = result.Email,
                Role = result.Role
            });
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("create-teacher")]
        public async Task<IActionResult> CreateTeacher(CreateStaffDto dto)
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (adminIdClaim == null)
                return Unauthorized("Admin ID not found.");

            if (!Guid.TryParse(adminIdClaim.Value, out Guid adminId))
                return Unauthorized("Invalid Admin ID.");

            var result = await _authService.CreateTeacherAsync(dto, adminId);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new CreateUserResponseDto
            {
                Message = result.Message,
                Email = result.Email,
                Role = result.Role
            });
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("create-specialist")]
        public async Task<IActionResult> CreateSpecialist(CreateStaffDto dto)
        {
            var adminIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (adminIdClaim == null)
                return Unauthorized("Admin ID not found.");

            if (!Guid.TryParse(adminIdClaim.Value, out Guid adminId))
                return Unauthorized("Invalid Admin ID.");

            var result = await _authService.CreateSpecialistAsync(dto, adminId);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(new CreateUserResponseDto
            {
                Message = result.Message,
                Email = result.Email,
                Role = result.Role
            });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
            ForgetPasswordRequestDto dto)
        {
            var result = await _authService.ForgetPasswordAsync(dto);

            return Ok(result);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(
            VerifyOtpRequestDto dto)
        {
            var result = await _authService.VerfiyOtpAsync(dto);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
            ResetPasswordRequestDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);

            return Ok(result);
        }
    
    }
}