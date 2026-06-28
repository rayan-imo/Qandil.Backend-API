using Qandil.Core.Common;
using Qandil.Service.AuthServices.Helper.Dtos;
using Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request;
using Qandil.Services.AuthServices.Helper;

namespace Qandil.Services.AuthServices.Services
{
    public interface IAuthService
    {
        public Task<AuthModel> RegisterAsync(RegisterModel model);
        public Task<AuthModel> LogInAsync(LogInModel model);
        public Task<AuthModel> CreateAdminAsync(CreateAdminDto dto);
        public Task<Result<string>> ForgetPasswordAsync(ForgetPasswordRequestDto dto);
        public Task<Result<string>> VerfiyOtpAsync(VerifyOtpRequestDto dto);
        public Task<Result<string>> ResetPasswordAsync(ResetPasswordRequestDto dto);

    }
}
