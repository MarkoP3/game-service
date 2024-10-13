using GameService.Application.Abstractions;
using GameService.Application.Dtos;
using GameService.Application.Game.v1.Queries;
using GameService.Domain.Enums;
using MediatR;

namespace GameService.Application.Game.v1.Commands;

internal sealed class PlayGameCommandHandler(ISender sender, IChoiceRepository choiceRepository) : ICommandHandler<PlayGameCommand, GameResultResponse>
{
    public async Task<GameResultResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var getRandomChoiceTask = sender.Send(new GetRandomChoiceQuery(), cancellationToken);
        var getChoiceByIdTask = choiceRepository.GetChoiceByIdAsync(request.ChoiceId, cancellationToken);

        await Task.WhenAll(getChoiceByIdTask, getRandomChoiceTask);

        var randomComputerChoiceResponse = await getRandomChoiceTask;
        var playerChoice = await getChoiceByIdTask;

        ArgumentNullException.ThrowIfNull(randomComputerChoiceResponse);
        ArgumentNullException.ThrowIfNull(playerChoice);


        return new(
            (GameResult)playerChoice.Compare(randomComputerChoiceResponse.Id),
            playerChoice.Id,
            randomComputerChoiceResponse.Id);
    }
}
