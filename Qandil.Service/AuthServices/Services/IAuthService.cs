using Qandil.Services.AuthServices.Helper;

namespace Qandil.Services.AuthServices.Services
{
    public interface IAuthService
    {
        public Task<AuthModel> RegisterAsync(RegisterModel model);
        public Task<AuthModel> LogInAsync(LogInModel model);

    }
}
