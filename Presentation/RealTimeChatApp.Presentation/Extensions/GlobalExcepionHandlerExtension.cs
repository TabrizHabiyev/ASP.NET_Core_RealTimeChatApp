using RealTimeChatApp.Presentation.Middlewares;

namespace RealTimeChatApp.Presentation.Extensions;

public static class GlobalExcepionHandlerExtension
{
    public static void UseGlobalExcepionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExcepionHandler>();
    }
}
