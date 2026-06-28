using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class UserOtp : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddDays(1);

    }
}
