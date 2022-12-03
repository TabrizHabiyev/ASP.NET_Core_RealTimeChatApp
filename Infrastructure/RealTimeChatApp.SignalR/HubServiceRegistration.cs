using Microsoft.AspNetCore.Builder;
using RealTimeChatApp.SignalR.Hubs;

public static class HubServiceRegistration
{
    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<ChatHub>("/chatHub");
    }
}
