using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.SignalR
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IChatHubService, ChatHubService>();
        }
    }
}
