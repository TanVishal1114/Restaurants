
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares
{
    public class ErrorHanlingMiddleware(ILogger<ErrorHanlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(new { Error = notFound.Message });
                logger.LogWarning(notFound, notFound.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception has occurred while processing the request.");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { Error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
