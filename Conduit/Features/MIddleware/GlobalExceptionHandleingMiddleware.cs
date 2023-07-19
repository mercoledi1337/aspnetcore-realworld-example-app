using Conduit.Features.Weather.UI;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.Json;

namespace Conduit.Features.MIddleware
{
    public class GlobalExceptionHandleingMiddleware
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
            try
            {
                await _next(context);
                await HandleExceptionAsync(context);
            }
            //
            catch (ArgumentException ex)
            {
                
                await HandleExceptionAsync(context, ex);
                
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, ArgumentException ex = null) 
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
            if (ex != null)
            {
                if (ex.Message == "za długie")
                {
                    var code = HttpStatusCode.BadRequest;
                    var result = JsonSerializer.Serialize(new { error = ex.Message });
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)code;
                    await context.Response.WriteAsync(result);
                    
                }
            }
            _logger.LogInformation($"Request failure dsadsadsadsa ==== {context.Response.StatusCode} ==== ");

        }
    }
}