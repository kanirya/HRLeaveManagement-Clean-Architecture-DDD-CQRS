using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception occurred");

            var statusCode = HttpStatusCode.InternalServerError;
            object responseBody;

            switch (exception)
            {
                case BadRequestException badRequest:
                    statusCode = HttpStatusCode.BadRequest;
                    responseBody = new
                    {
                        statusCode = (int)statusCode,
                        message = badRequest.Message
                    };
                    break;

                case NotFoundException notFound:
                    statusCode = HttpStatusCode.NotFound;
                    responseBody = new
                    {
                        statusCode = (int)statusCode,
                        message = notFound.Message
                    };
                    break;

                case ValidationExceptions validation:
                    statusCode = HttpStatusCode.BadRequest;
                    responseBody = new
                    {
                        statusCode = (int)statusCode,
                        message = "Validation failed",
                        errors = validation.Errors
                    };
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    responseBody = new
                    {
                        statusCode = (int)statusCode,
                        message = "Access denied"
                    };
                    break;

                default:
                    responseBody = new
                    {
                        statusCode = (int)statusCode,
                        message = "An unexpected error occurred. Please try again later.",
                        details = _env.IsDevelopment() ? exception.Message : null
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody, options));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
