using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.Users
{
    public class UserRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RoleType Role { get; set; }
    }
}
