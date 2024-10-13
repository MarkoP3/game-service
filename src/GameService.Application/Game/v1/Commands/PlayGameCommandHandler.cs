using GameService.Application.Abstractions;
using GameService.Application.Dtos;
using GameService.Application.Game.v1.Queries;
using GameService.Domain.Enums;
using GameService.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GameService.Application.Game.v1.Commands;

internal sealed class PlayGameCommandHandler(ISender sender,
    IChoiceRepository choiceRepository,
    ILogger<PlayGameCommandHandler> logger)
    : ICommandHandler<PlayGameCommand, GameResultResponse>
{
    public async Task<GameResultResponse> Handle(PlayGameCommand request, CancellationToken cancellationToken)
    {
        var getRandomChoiceTask = sender.Send(new GetRandomChoiceQuery(), cancellationToken);

        var getChoiceByIdTask = choiceRepository.GetChoiceByIdAsync(request.ChoiceId, cancellationToken);

        await Task.WhenAll(getChoiceByIdTask, getRandomChoiceTask);

        var randomComputerChoiceResponse = await getRandomChoiceTask;

        var playerChoice = await getChoiceByIdTask ?? throw new ChoiceNotFoundException(choiceId: request.ChoiceId);

        ArgumentNullException.ThrowIfNull(randomComputerChoiceResponse);

        var gameResult = (GameResult)playerChoice.Compare(randomComputerChoiceResponse.Id);

        logger.LogInformation("Game initiate player chose {PlayerChoice} and computer chose {ComputerChoice}. Game Result {Result}",
            playerChoice.Name,
            randomComputerChoiceResponse.Name,
            gameResult.ToString());

        return new(gameResult, playerChoice.Id, randomComputerChoiceResponse.Id);
    }
}
