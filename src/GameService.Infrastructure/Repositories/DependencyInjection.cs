using GameService.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameService.Infrastructure.Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<IChoiceRepository, ChoiceRepository>();
        services.AddDbContext<GameDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("GameDb")),
            ServiceLifetime.Transient);
        return services;
    }
}
