using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Qandil.API.Filters
{
    public class HttpResponseExceptionFilter : IExceptionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnException(ExceptionContext context)
        {
            var (statusCode, title) = context.Exception switch
            {
                ValidationException => (HttpStatusCode.BadRequest, "Validation Error"),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized"),
                KeyNotFoundException => (HttpStatusCode.NotFound, "Not Found"),
                NotImplementedException => (HttpStatusCode.NotImplemented, "Not Implemented"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = context.Exception.Message,
                Type = GetProblemDetailsType(statusCode),
                Instance = context.HttpContext.Request.Path
            };

            // Add validation errors if available
            if (context.Exception is ValidationException validationEx && validationEx.ValidationResult != null)
            {
                problemDetails.Extensions["errors"] = new Dictionary<string, string[]>
            {
                { validationEx.ValidationResult.MemberNames.FirstOrDefault() ?? "General",
                  new[] { validationEx.ValidationResult.ErrorMessage } }
            };
            }

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = (int)statusCode
            };

            context.ExceptionHandled = true;
        }

        private static string GetProblemDetailsType(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                HttpStatusCode.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
                HttpStatusCode.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                HttpStatusCode.InternalServerError => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                _ => "https://tools.ietf.org/html/rfc7231"
            };
        }
    }
}
