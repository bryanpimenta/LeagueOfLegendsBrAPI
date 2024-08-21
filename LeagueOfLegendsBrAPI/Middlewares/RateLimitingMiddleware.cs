using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AspNetCoreRateLimit;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitingMiddleware> _logger;

    public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
        {
            _logger.LogWarning("Rate limit exceeded for IP: {IP}", context.Connection.RemoteIpAddress.ToString());
            context.Response.ContentType = "application/json";
            var response = new
            {
                message = "Limite de Request excedido. Espere 1 minuto e tente novamente. Agradeço :)"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
