namespace Qandil.Service.AuthServices.Helper.Dtos.OtpDto.Request
{
    public class VerifyOtpRequestDto
    {
        public string Email { get; set; }
        public string Otp { get; set; }
    }
}
