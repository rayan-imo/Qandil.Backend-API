using Qandil.Core.Common;
using Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request;
using Qandil.Service.AuthServices.Helper.Dtos.Requests;
using Qandil.Service.AuthServices.Helper.Dtos.Responses;
using Qandil.Services.AuthServices.Helper;

namespace Qandil.Services.AuthServices.Services
{
    public interface IAuthService
    {
        public Task<AuthModel> RegisterAsync(RegisterModel model);
        public Task<AuthModel> LogInAsync(LogInModel model);
        public Task<CreateUserResponseDto> CreateAdminAsync(CreateAdminDto dto);
        public Task<Result<string>> ForgetPasswordAsync(ForgetPasswordRequestDto dto);
        public Task<Result<string>> VerfiyOtpAsync(VerifyOtpRequestDto dto);
        public Task<Result<string>> ResetPasswordAsync(ResetPasswordRequestDto dto);
        Task<CreateUserResponseDto> CreateTeacherAsync(CreateStaffDto dto, Guid adminId);
        Task<CreateUserResponseDto> CreateSpecialistAsync(CreateStaffDto dto,Guid adminId);

    }
}
