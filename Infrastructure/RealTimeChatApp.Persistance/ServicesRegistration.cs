using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.UnitOfWork;

namespace RealTimeChatApp.Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
    }
}
