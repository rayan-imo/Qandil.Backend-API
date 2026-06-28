using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos
{
    public class CreateAdminDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(50)]
        public required string Email { get; set; }

        [MinLength(8), MaxLength(10)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
               ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.")]
        public required string Password { get; set; }

    }
}
