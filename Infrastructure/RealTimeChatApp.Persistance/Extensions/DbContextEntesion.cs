using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RealTimeChatApp.Domain.Enums;


namespace RealTimeChatApp.Persistance.Extensions;

    internal static class DbContextEntesion 
    {
        internal static void ApplicationDbContext<TContexts>(this IServiceCollection services, string connectionString , DatabaseType DatabaseType)where TContexts : DbContext 
        {
            services.AddDbContext<TContexts>( options => _ = DatabaseType switch
            {
                DatabaseType.SqlServer => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(TContexts).Assembly.FullName)),
                DatabaseType.SqlServerLocalDb => options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(TContexts).Assembly.FullName)),
                DatabaseType.MySql => options.UseMySql(connectionString,null,mySqlOptions => {mySqlOptions.MigrationsAssembly( typeof(TContexts).Assembly.FullName);}),
                DatabaseType.PosgerSql => options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(TContexts).Assembly.FullName)),
                DatabaseType.InMemory => options.UseInMemoryDatabase("RealTimeChatAppDb"),
                _ => throw new ArgumentOutOfRangeException(nameof(DatabaseType), DatabaseType, null)
            });
        }
}


