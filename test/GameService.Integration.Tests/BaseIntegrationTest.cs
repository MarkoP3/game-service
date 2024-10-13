using GameService.Infrastructure;
using GameService.Infrastructure.Seeders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Integration.Tests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly GameDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory webAppFactory)
    {
        _scope = webAppFactory.Services.CreateAsyncScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<GameDbContext>();
        DbContext.MigrateAndSeed();
    }

}
