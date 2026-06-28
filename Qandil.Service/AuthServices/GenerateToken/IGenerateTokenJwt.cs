using Qandil.Core.Enums;

namespace Qandil.Services.AuthServices.GenerateToken;

public interface  IGenerateTokenJwt
{
    public string GenerateAccessToken(Guid userId, RoleType role,string? email = null);
}
