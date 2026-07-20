using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class ResetPasswordRequestDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(50),Required]
        public string Email {  get; set; }

        [MaxLength(50),Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
                ErrorMessage = "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.")]
        public string NewPassword { get;set;}
    }
}
