using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Persistance.Contexts;
using RealTimeChatApp.Persistance.Services;

namespace RealTimeChatApp.Persistance;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

        services.AddDbContext<RealTimeChatAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        builder => builder.MigrationsAssembly(typeof(RealTimeChatAppDbContext).Assembly.FullName)));
        
        #region Identity services configure
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<RealTimeChatAppDbContext>();
            
            services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromMinutes(60));
        #endregion

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
