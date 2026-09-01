using Qandil.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class ForgetPasswordRequestDto
    {
        [MaxLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public required string Email { get; set; }

    }
}
