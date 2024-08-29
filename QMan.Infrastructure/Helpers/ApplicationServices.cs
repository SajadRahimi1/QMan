using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QMan.Infrastructure.Contexts;
using QMan.Infrastructure.Interfaces;
using QMan.Infrastructure.Repositories;

namespace QMan.Infrastructure.Helpers;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddTransient<ITicketRepository, TicketRepository>();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
        return services;
    }

}