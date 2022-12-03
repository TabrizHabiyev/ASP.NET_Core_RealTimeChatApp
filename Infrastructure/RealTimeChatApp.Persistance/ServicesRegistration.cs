using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Domain.Enums;
using RealTimeChatApp.Persistance.Contexts;
using RealTimeChatApp.Persistance.Extensions;
using RealTimeChatApp.Persistance.Repositories;
using RealTimeChatApp.Persistance.Services;

namespace RealTimeChatApp.Persistance;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();

        #region Connection to database
        // connection string
        string connectionString = configuration.GetConnectionString("DefaultConnection")??string.Empty;
        // check sql server type
        DatabaseType sqlServerType = configuration.GetValue<DatabaseType>("DatabaseType");
        services.ApplicationDbContext<RealTimeChatAppDbContext>(connectionString, sqlServerType);
        #endregion



        #region Identity services configure
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<RealTimeChatAppDbContext>();

        services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromMinutes(60));


        #endregion

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IChatRepository, ChatRepository>();

        services.AddScoped<IMessageRepository, MessageRepository>();

        services.AddScoped<IReactionRepository, ReactionRepository>();

        services.AddTransient<IChatService, ChatService>();

    }
}
