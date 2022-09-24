using DiscussionNet.Application.Common.Exceptions;
using DiscussionNet.Application.Common.Extensions;
using DiscussionNet.Infrastructure.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DiscussionNet.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex, ILogger logger)
        {
            httpContext.Response.ContentType = "application/json";
            switch (ex)
            {
                case ValidationException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return httpContext.Response.WriteAsync(new ValidationExceptionResponse()
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        ValitationErrors = ((ValidationException)ex).ValidationErrors
                    }.ToJSON());
                default:
                    var traceId = Guid.NewGuid().ToString().Substring(0, 5);
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    string message = $"Bir hata oluştu.";
                    logger.LogError(ex, message+" Error Code: "+ traceId);
                    return httpContext.Response.WriteAsync(new ExceptionResponse()
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = ex.Message,
                        ErrorCode = traceId
                    }.ToJSON());
            }

        }
    }
}
