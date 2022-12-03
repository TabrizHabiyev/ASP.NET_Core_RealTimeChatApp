

public static class HubServiceRegistration
{
    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<ChatHubService>("/chatHub");
    }
}
