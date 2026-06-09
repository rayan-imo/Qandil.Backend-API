namespace Qandil.Core.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string MessageAr { get; set; } = string.Empty;
        public string MessageEn { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResult(T data, string messageAr = "تمت العملية بنجاح",
             string messageEn = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                MessageAr = messageAr,
                MessageEn = messageEn,
                Data = data
            };
        }
        public static ApiResponse<T> FailResult( string messageAr,string messageEn)
        {
            return new ApiResponse<T>
            {
                Success = false,
                MessageAr = messageAr,
                MessageEn = messageEn,
                Data = default
            };
        }
    }
}

