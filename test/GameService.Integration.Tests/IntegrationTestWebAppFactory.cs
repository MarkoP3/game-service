using GameService.Application.Abstractions.ApiClients;
using GameService.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Runtime.InteropServices;
using Testcontainers.MsSql;

namespace GameService.Integration.Tests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbTestContainer;
    public static readonly Mock<IRandomNumberGeneratorApiClient> MockRandomNumberGeneratorApiClient = new();
    public IntegrationTestWebAppFactory()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _dbTestContainer = new MsSqlBuilder()
                .WithImage(
                    "mcr.microsoft.com/mssql/server:2022-latest"
                )
                .WithPortBinding(1433, true)
                .Build();
        }
        else
        {
            _dbTestContainer = new MsSqlBuilder()
                .WithPortBinding(1433, true)
                .Build();
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services
            .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<GameDbContext>));

            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }

            var randomNumberGeneratorApiClientDescriptor = services
            .SingleOrDefault(service => service.ServiceType == typeof(IRandomNumberGeneratorApiClient));

            if (randomNumberGeneratorApiClientDescriptor is not null)
            {
                services.Remove(randomNumberGeneratorApiClientDescriptor);
            }

            services.AddTransient((_) => MockRandomNumberGeneratorApiClient.Object);

            services.AddDbContext<GameDbContext>(options => options.UseSqlServer(
                _dbTestContainer.GetConnectionString()),
                ServiceLifetime.Transient);
        });
    }
    public Task InitializeAsync() => _dbTestContainer.StartAsync();

    public new Task DisposeAsync() => _dbTestContainer.StopAsync();
}
