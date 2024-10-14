using GameService.Application.Abstractions;
using GameService.Application.Dtos;
using GameService.Domain.Exceptions;

namespace GameService.Application.Game.v1.Queries;

internal sealed class GetChoicesQueryHandler(IChoiceRepository choiceRepository)
    : IQueryHandler<GetChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public async Task<IEnumerable<ChoiceResponse>> Handle(GetChoicesQuery request, CancellationToken cancellationToken)
    {
        var choices = await choiceRepository.GetAllChoicesAsync(cancellationToken);

        if (choices is null || !choices.Any())
            throw new NoAvailableChoicesException();

        return choices.Select(choice => new ChoiceResponse(choice));
    }
}
