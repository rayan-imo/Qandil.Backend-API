namespace Qandil.Service.Helpers
{
    public static class PhoneFormatter
    {
        //private static bool BeValidSyrainPhone(string phone)
        //{
        //    if (string.IsNullOrWhiteSpace(phone)) return false;
        //    phone = phone.Trim();
        //    return phone.StartsWith("+963") || phone.StartsWith("0");
        //}
        public static string FormatSyiranPhone(string phone)
        {
            phone = phone.Trim();
            if (phone.StartsWith("0"))
            {
                phone = phone.Substring(1);
                return "+963" + phone;
            }
           
                return "+963"+phone;
        }
    }
}
