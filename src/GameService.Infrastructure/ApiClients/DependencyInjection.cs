using GameService.Application.Abstractions.ApiClients;
using GameService.Application.Options.ApiClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Refit;

namespace GameService.Infrastructure.ApiClients;

public static class DependencyInjection
{
    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        services.AddOptions<RandomNumberGeneratorApiClientOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(RandomNumberGeneratorApiClientOptions.RandomNumberApi).Bind(settings);
            });

        services.AddRefitClient<IRandomNumberGeneratorApiClient>()
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)))
            .ConfigureHttpClient((serviceProvided, httpClient) =>
            {
                var options = serviceProvided.GetRequiredService<IOptions<RandomNumberGeneratorApiClientOptions>>().Value;
                httpClient.BaseAddress = options.BaseUrl;
            });

        return services;
    }
}
