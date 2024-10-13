using GameService.Application.Abstractions;
using GameService.Application.Abstractions.ApiClients;
using GameService.Application.Dtos;

namespace GameService.Application.Game.v1.Queries;

internal sealed class GetRandomChoiceQueryHandler(IChoiceRepository choiceRepository,
    IRandomNumberGeneratorApiClient randomNumberGeneratorApiClient)
    : IQueryHandler<GetRandomChoiceQuery, ChoiceResponse>
{
    public async Task<ChoiceResponse> Handle(GetRandomChoiceQuery request, CancellationToken cancellationToken)
    {
        var getRandomNumberTask = randomNumberGeneratorApiClient.GenerateRandomNumberFromOneToOneHundredAsync(cancellationToken);
        var getChoiceCountTask = choiceRepository.GetChoicesCountAsync(cancellationToken);
        await Task.WhenAll(getRandomNumberTask, getChoiceCountTask);

        var randomNumber = await getRandomNumberTask;
        var choiceCount = await getChoiceCountTask;

        var choiceIndex = randomNumber.Value % choiceCount;
        var choice = await choiceRepository.GetChoiceAtIndexAsync(choiceIndex, cancellationToken);
        return new(choice);
    }
}
