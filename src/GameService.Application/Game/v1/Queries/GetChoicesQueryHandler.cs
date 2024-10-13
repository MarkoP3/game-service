using GameService.Application.Abstractions;
using GameService.Application.Dtos;

namespace GameService.Application.Game.v1.Queries;

internal sealed class GetChoicesQueryHandler(IChoiceRepository choiceRepository) 
    : IQueryHandler<GetChoicesQuery, IEnumerable<ChoiceResponse>>
{
    public async Task<IEnumerable<ChoiceResponse>> Handle(GetChoicesQuery request, CancellationToken cancellationToken) 
        => (await choiceRepository.GetAllChoicesAsync(cancellationToken))
        .Select(choice => new ChoiceResponse(choice));
}
