using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace Conduit.Features.MIddleware
{
    public class GlobalExceptionHandleingMiddleware : Exception
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandleingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
                HandleExceptionAsync(context);

        }

        private async Task HandleExceptionAsync(HttpContext context) 
        {
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