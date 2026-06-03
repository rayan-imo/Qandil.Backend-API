using System.Diagnostics;

namespace Qandil.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var request = context.Request;
            _logger.LogInformation("Incoming Request: {method} {path}{query}",
                request.Method,
                request.Path,
                request.QueryString);

            await _next(context); // Call the next middleware

            stopwatch.Stop();

            var response = context.Response;
            _logger.LogInformation("Response: {statusCode} for {method} {path} in {elapsed}ms",
                response.StatusCode,
                request.Method,
                request.Path,
                stopwatch.ElapsedMilliseconds);
        }
    }
}

