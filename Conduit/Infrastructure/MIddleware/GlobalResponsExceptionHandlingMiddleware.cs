using System.Net;
using System.Text.Json;

namespace Conduit.Features.MIddleware
{
    public class GlobalExceptionHandleingMiddleware : SystemException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandleingMiddleware> _logger;

        public GlobalExceptionHandleingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandleingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
               
                _logger.LogInformation($"====={context.Response.StatusCode}====");
                await HandleExceptionAuthorizeAsync(context);
            }
        }

        private async Task HandleExceptionAuthorizeAsync(HttpContext context) 
        {
            //tu można jakoś lepiej zrobić
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                var code = HttpStatusCode.Unauthorized;
                var result = JsonSerializer.Serialize(new { error = "Unauthorized" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(result);

            }
        }
    }
}