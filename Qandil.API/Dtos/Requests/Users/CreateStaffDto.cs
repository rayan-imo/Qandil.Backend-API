using Qandil.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Qandil.API.Dtos.Requests.Users
{
    public class CreateStaffDto
    {
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public required string Email { get; set; }

        [MaxLength(10)]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,10}$",
   ErrorMessage = "Password must be between 8 and 10 characters and include uppercase, lowercase, number, and special character.")]
        public required string Password { get; set; }
        public RoleType Role { get; set; }
       
    }
}

