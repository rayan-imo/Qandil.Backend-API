using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class User:BaseEntity
    {
       // public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RoleType Role { get; set; }

    }
}
