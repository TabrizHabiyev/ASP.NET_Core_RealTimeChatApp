

namespace RealTimeChatApp.Presentation.Middlewares;

public class GlobalExcepionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExcepionHandler> _logger;

    public GlobalExcepionHandler(RequestDelegate next, ILogger<GlobalExcepionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        if (exception is GlobalEx.NotFoundException) code = HttpStatusCode.NotFound;
        else if (exception is GlobalEx.UnauthorizedException) code = HttpStatusCode.Unauthorized;
        else if (exception is GlobalEx.ForbiddenException) code = HttpStatusCode.Forbidden;
        else if (exception is GlobalEx.BadRequestException) code = HttpStatusCode.BadRequest;
        else if (exception is GlobalEx.ConflictException) code = HttpStatusCode.Conflict;
        else if (exception is GlobalEx.NotImplementedException) code = HttpStatusCode.NotImplemented;

        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(result);
    }
}
