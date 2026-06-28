using Qandil.Core.Entity;

namespace Qandil.Service.AuthServices.Helper.EmailTemplates
{
    public static class EmailTemplate
    {
        public static string ResetPAsswordOtp(string otp)
        {
            return $@"
              Hello,<br/><br/>
           
             We received a request to reset your password for your Qandil account.<br/><br/>
           
            <b>Your verification code is: {otp}</b><br/><br/>
           
            This code will expire in <b>5 minutes</b>.<br/><br/>
           
            If you did not request a password reset, please ignore this email.<br/><br/>
           
               Best regards,<br/>
                QandilTeam";

        }
    }
}

