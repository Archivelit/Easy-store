namespace API.Middleware.Logging;

public class RequestLogger
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLogger> _logger;
    
    public RequestLogger(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(DateTime.Now + $"Request: {context.Request.Method} {context.Request.Path}");
        
        await _next(context);
        
        _logger.LogInformation(DateTime.Now + $"Response: {context.Response.StatusCode}");
    }
}