using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QMan.Application.Dtos.Base;
using QMan.Infrastructure.Contexts;
using QMan.Infrastructure.Interfaces;
using QMan.Infrastructure.Repositories;

namespace QMan.Infrastructure.Helpers;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddSingleton<IFileRepository,FileRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });
        ConfigurationModel.Instance.JwtToken = configuration["Jwt:Key"]??"";
        ConfigurationModel.Instance.Audience = configuration["Jwt:Audience"]??"";
        ConfigurationModel.Instance.Issuer = configuration["Jwt:Issuer"]??"";
        return services;
    }

}