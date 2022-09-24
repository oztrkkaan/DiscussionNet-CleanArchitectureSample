using Microsoft.AspNetCore.Builder;

namespace DiscussionNet.Infrastructure.Middlewares
{
    public static class MiddlewareConfigurationExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
