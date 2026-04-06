
using System.Net;
using System.Text.Json;

namespace StudentManagementAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next , ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid Request");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { error = "something want wrong" });
                await context.Response.WriteAsync(result);
            }
        }


    }
}
