using Qandil.Core.Enums;

namespace Qandil.Services.AuthServices.Helper;

public class AuthModel
{
    public string Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public RoleType Role { get; set; }
    public DateTime ExpiresOn { get; set; }
}
