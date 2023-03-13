using System.Collections.Concurrent;

namespace OnlineStore.WebApi.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly ConcurrentDictionary<string, int> _concurrent=new();

    public RequestLoggingMiddleware(RequestDelegate requestDelegate, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        if (ctx == null) throw new ArgumentNullException(nameof(ctx));
        
        var requestPath = ctx.Request.Path.ToString();
        if (requestPath.EndsWith("/metrics"))
        {
            if (!ctx.Response.HasStarted)
            {
                await ctx.Response.WriteAsJsonAsync(_concurrent);
            }
        }
        else
        {
            _concurrent.AddOrUpdate(
                ctx.Request.Path, _ => 1, (_, currentCount) => currentCount+1);  
            _logger.LogInformation("Request Method: {Method}",ctx.Request.Method);
            await _next(ctx);
        }
       
    }
}