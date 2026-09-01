using Qandil.API.Dtos.Responses.Tests;
using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.Users
{
    public class UserResponse
    {
        public string? Email { get; set; }
        public RoleType Role { get; set; }
        public static UserResponse Transform(User user)
        {
            return new UserResponse
            {Email=user.Email,
            Role=user.Role};

        }
    }
}
