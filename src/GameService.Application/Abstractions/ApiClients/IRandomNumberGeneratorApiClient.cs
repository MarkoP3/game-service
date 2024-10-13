using GameService.Application.Dtos;
using Refit;

namespace GameService.Application.Abstractions.ApiClients;

public interface IRandomNumberGeneratorApiClient
{
    [Get("/random")]
    Task<RandomNumberResponse> GenerateRandomNumberFromOneToOneHundredAsync(CancellationToken cancellationToken);
}
