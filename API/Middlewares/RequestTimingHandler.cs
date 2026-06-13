
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace API.Middlewares
{
    public class RequestTimingHandler : IMiddleware
    {
        private readonly ILogger<RequestTimingHandler> _logger;
        public RequestTimingHandler(ILogger<RequestTimingHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            context.Response.OnStarting(() =>
            {
                var durationMs = stopwatch.Elapsed.TotalMilliseconds;
                context.Response.Headers.Append("X-Request-Duration", durationMs.ToString("F2")); // Format lấy 2 chữ số thập phân
                return Task.CompletedTask;
            });

            await next(context);

            stopwatch.Stop();
            var totalDurationMs = stopwatch.Elapsed.TotalMilliseconds; // Hoặc stopwatch.Elapsed.TotalMilliseconds tùy phiên bản .NET

            _logger.LogInformation("Request duration: {Duration} ms", totalDurationMs.ToString("F2"));

            if (totalDurationMs > 1000)
            {
                _logger.LogWarning("Request duration is too long: {Duration} ms", totalDurationMs.ToString("F2"));
            }
        }
    }
}
