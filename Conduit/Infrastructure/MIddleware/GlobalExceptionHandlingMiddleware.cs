using System.Net;
using System.Text.Json;

namespace Conduit.Features.MIddleware
{
    public class GlobalResponsExceptionHandlingMiddleware
    {
            private readonly RequestDelegate _next;
            private readonly ILogger<GlobalResponsExceptionHandlingMiddleware> _logger;

            public GlobalResponsExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalResponsExceptionHandlingMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }
            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (ArgumentException ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }
            private async Task HandleExceptionAsync(HttpContext context, ArgumentException ex)
            {
                    var code = HttpStatusCode.BadRequest;
                    var result = JsonSerializer.Serialize(new { error = ex.Message });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    await context.Response.WriteAsync(result);
            }
        }
}