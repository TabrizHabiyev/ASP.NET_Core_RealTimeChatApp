using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RealTimeChatApp.Persistance.Contexts;
using RealTimeChatApp.Domain.Enums;

namespace RealTimeChatApp.Persistance;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RealTimeChatAppDbContext>
{
    public RealTimeChatAppDbContext CreateDbContext(string[] args)
    {
       // get the configuratin json file path 
        var _configuration = new ConfigurationBuilder()
            .SetBasePath("C:\\Users\\tabri\\OneDrive\\Desktop\\ASP.NET_Core_RealTimeChatApp\\Presentation\\RealTimeChatApp.Presentation")
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = _configuration.GetConnectionString("DefaultConnection")??string.Empty;

        DatabaseType sqlServerType = _configuration.GetValue<DatabaseType>("DatabaseType");

        DbContextOptionsBuilder<RealTimeChatAppDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder =  _ = sqlServerType switch{
            DatabaseType.SqlServer => dbContextOptionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(RealTimeChatAppDbContext).Assembly.FullName)),
            DatabaseType.SqlServerLocalDb => dbContextOptionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(RealTimeChatAppDbContext).Assembly.FullName)),
            DatabaseType.MySql => dbContextOptionsBuilder.UseMySql(connectionString,null,mySqlOptions => {mySqlOptions.MigrationsAssembly( typeof(RealTimeChatAppDbContext).Assembly.FullName);}),
            DatabaseType.PosgerSql => dbContextOptionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(RealTimeChatAppDbContext).Assembly.FullName)),
            DatabaseType.InMemory => dbContextOptionsBuilder.UseInMemoryDatabase("RealTimeChatAppDb"),
            _ => throw new ArgumentOutOfRangeException(nameof(DatabaseType), sqlServerType, null)
        };
        return new(dbContextOptionsBuilder.Options);
    }
}

