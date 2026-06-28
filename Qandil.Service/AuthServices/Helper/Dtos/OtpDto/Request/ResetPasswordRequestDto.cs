using Microsoft.Identity.Client;

namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class ResetPasswordRequestDto
    { public string Email {  get; set; }
       public string NewPassword { get;set;}
    }
}
