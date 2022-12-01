using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.Common.Interfaces.Token;
using RealTimeChatApp.Infrastructure.Services.Email;
using RealTimeChatApp.Infrastructure.Services.Token;

namespace RealTimeChatApp.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenHandler, TokenHadler>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();


        #region Jwt Bearer Configuration
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = configuration["Token:Audience"],
                ValidIssuer = configuration["Token:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameteres) => expires != null ? expires > DateTime.UtcNow : false,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
            };
        });
        #endregion

        return services;
    }
}
