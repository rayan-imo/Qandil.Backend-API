using Qandil.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos.Requests
{
    public class CreateAdminDto
    {
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [MaxLength(10)]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,10}$",
   ErrorMessage = "Password must be between 8 and 10 characters and include uppercase, lowercase, number, and special character.")]
        public required string Password { get; set; }

    }
}
