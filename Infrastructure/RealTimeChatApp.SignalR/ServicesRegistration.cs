

namespace RealTimeChatApp.SignalR
{
    public static class ServicesRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddTransient<IChatHubService, ChatHubService>();
            services.AddSignalR();
        }
    }
}
