using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.UserDto.Request
{
    public class UserRequestdto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RoleType Role { get; set; }

    }
}
