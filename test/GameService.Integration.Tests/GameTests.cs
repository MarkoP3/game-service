using GameService.Application.Dtos;
using GameService.Application.Game.v1.Commands;
using GameService.Application.Game.v1.Queries;
using GameService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GameService.Integration.Tests;

public class GameTests(IntegrationTestWebAppFactory webAppFactory)
    : BaseIntegrationTest(webAppFactory)
{
    [Fact]
    public async Task GetChoices_GetAll_ShouldReturnAllChoices()
    {
        //arrange
        var query = new GetChoicesQuery();

        //act
        var getChoicesTask = Sender.Send(query);
        var getChoicesFromDbTask = DbContext.Choices.ToListAsync();
        await Task.WhenAll(getChoicesTask, getChoicesFromDbTask);
        var choices = await getChoicesTask;
        var choicesFromDb = await getChoicesFromDbTask;

        //assert
        Assert.Equal(choicesFromDb.Count, choices.Count());
        Assert.All(choices, choice =>
        {
            var choiceFromDb = choicesFromDb.Find(c => c.Id == choice.Id);
            Assert.NotNull(choiceFromDb);
            Assert.Equal(choiceFromDb.Name, choice.Name);
        });
    }

    [Theory]
    [InlineData(10, "Rock")]
    [InlineData(11, "Paper")]
    [InlineData(12, "Scissors")]
    [InlineData(13, "Spock")]
    [InlineData(14, "Lizard")]
    public async Task GetRandomChoice_SeededRandomAndInRangeOf1To100_ShouldReturnExpectedChoice(
        int seededRandomNumber,
        string expectedChoiceName
        )
    {
        //arrange
        var query = new GetRandomChoiceQuery();
        IntegrationTestWebAppFactory.MockRandomNumberGeneratorApiClient
            .Setup(c => c.GenerateRandomNumberFromOneToOneHundredAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RandomNumberResponse(seededRandomNumber));

        //act
        var randomChoice = await Sender.Send(query);

        //assert
        Assert.Equal(expectedChoiceName, randomChoice.Name);
    }


    [Theory]
    #region Computer playing Rock and player playing all other combinations in order(Rock,Paper,Scissors,Spock,Lizard)
    [InlineData(10, 1, GameResult.Tie)]
    [InlineData(10, 2, GameResult.Win)]
    [InlineData(10, 3, GameResult.Lose)]
    [InlineData(10, 4, GameResult.Win)]
    [InlineData(10, 5, GameResult.Lose)]
    #endregion
    #region Computer playing Paper and player playing all other combinations in order(Rock,Paper,Scissors,Spock,Lizard)
    [InlineData(11, 1, GameResult.Lose)]
    [InlineData(11, 2, GameResult.Tie)]
    [InlineData(11, 3, GameResult.Win)]
    [InlineData(11, 4, GameResult.Lose)]
    [InlineData(11, 5, GameResult.Win)]
    #endregion
    #region Computer playing Scissors and player playing all other combinations in order(Rock,Paper,Scissors,Spock,Lizard)
    [InlineData(12, 1, GameResult.Win)]
    [InlineData(12, 2, GameResult.Lose)]
    [InlineData(12, 3, GameResult.Tie)]
    [InlineData(12, 4, GameResult.Win)]
    [InlineData(12, 5, GameResult.Lose)]
    #endregion
    #region Computer playing Spock and player playing all other combinations in order(Rock,Paper,Scissors,Spock,Lizard)
    [InlineData(13, 1, GameResult.Lose)]
    [InlineData(13, 2, GameResult.Win)]
    [InlineData(13, 3, GameResult.Lose)]
    [InlineData(13, 4, GameResult.Tie)]
    [InlineData(13, 5, GameResult.Win)]
    #endregion
    #region Computer playing Lizard and player playing all other combinations in order(Rock,Paper,Scissors,Spock,Lizard)
    [InlineData(14, 1, GameResult.Win)]
    [InlineData(14, 2, GameResult.Lose)]
    [InlineData(14, 3, GameResult.Win)]
    [InlineData(14, 4, GameResult.Lose)]
    [InlineData(14, 5, GameResult.Tie)]
    #endregion
    public async Task PlayGame_AgainsComputerWithSeeded_ShouldReturnExpectedChoice(
    int seededRandomNumber,
    int playerChoiceId,
    GameResult expectedGameResult
    )
    {
        //arrange
        IntegrationTestWebAppFactory.MockRandomNumberGeneratorApiClient
            .Setup(c => c.GenerateRandomNumberFromOneToOneHundredAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RandomNumberResponse(seededRandomNumber));
        var command = new PlayGameCommand(playerChoiceId);

        //act
        var gameResult = await Sender.Send(command);

        //assert
        Assert.Equal(gameResult.Results, expectedGameResult);
    }
}
