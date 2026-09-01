using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class VerifyOtpRequestDto
    {
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public required string Email { get; set; }

        public required string Otp { get; set; }
    }
}
