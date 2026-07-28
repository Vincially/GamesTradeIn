using GamesTradeIn.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace GamesTradeIn.API.MIddlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is DomainException domainException)
        {
            logger.LogWarning("Business Rule Violated: {DomainExceptionMessage}", domainException?.Message);
            
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsJsonAsync(new
            {
                Type = "BusinessRuleError",
                Detail = domainException?.Message
            }, cancellationToken);
            return true;
        }
        
        logger.LogError(exception, "Unexpected error on server: {ExceptionMessage}", exception.Message);

        httpContext.Response.StatusCode = 500;
        await httpContext.Response.WriteAsJsonAsync(new
        {
            Type = "InternalServerError",
            Detail = exception?.Message
        }, cancellationToken);
        return true;
    }
}