using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Infrastructure.Services.Email;

namespace RealTimeChatApp.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSenderService,EmailSenderService>();
    }
}
