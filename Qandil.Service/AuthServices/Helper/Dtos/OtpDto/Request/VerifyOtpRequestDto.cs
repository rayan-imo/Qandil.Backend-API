using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class VerifyOtpRequestDto
    {
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [MaxLength(50)]
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
