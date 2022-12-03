using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.SignalR
{
    public class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IChatHubService, ChatHubService>();
        }
    }
}
