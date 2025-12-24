namespace SmartCraft.Api.Middlewares
{
    public class RequestLoggerMiddleware(RequestDelegate _next, ILogger<RequestLoggerMiddleware> _logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            await _next(context);

            var duration = DateTime.UtcNow - startTime;

            _logger.LogInformation("📊 {Method} {Path} - {StatusCode} - {Duration}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                duration.TotalMilliseconds);
        }
    }
}
