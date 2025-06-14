namespace API.Middleware.Logging;

public static class LoggingExtensions
{
    public static IApplicationBuilder UseLogging(this IApplicationBuilder builder) => builder.UseMiddleware<RequestLogger>(); 
}