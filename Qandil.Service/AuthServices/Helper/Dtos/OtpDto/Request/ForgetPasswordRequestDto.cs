using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class ForgetPasswordRequestDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(50),Required]
        public required string Email {  get; set; }
    }
}
