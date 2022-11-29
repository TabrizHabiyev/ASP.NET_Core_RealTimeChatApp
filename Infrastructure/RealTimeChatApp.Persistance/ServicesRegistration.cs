using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Persistance.Contexts;

namespace RealTimeChatApp.Persistance;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();



        services.AddDbContext<RealTimeChatAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            builder => builder.MigrationsAssembly(typeof(RealTimeChatAppDbContext).Assembly.FullName))
        );

        return services;
    }
}
