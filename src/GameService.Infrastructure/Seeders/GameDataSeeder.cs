using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GameService.Infrastructure.Seeders;

public static class GameDataSeeder
{
    public static Choice Rock => new()
    {
        Id = 1,
        Name = "Rock",
        WeakerChoices = []
    };
    public static Choice Paper => new()
    {
        Id = 2,
        Name = "Paper",
        WeakerChoices = []
    };
    public static Choice Scissors => new()
    {
        Id = 3,
        Name = "Scissors",
        WeakerChoices = []
    };
    public static Choice Spock => new()
    {
        Id = 4,
        Name = "Spock",
        WeakerChoices = []
    };
    public static Choice Lizard => new()
    {
        Id = 5,
        Name = "Lizard",
        WeakerChoices = []
    };

    public static void MigrateAndSeedData(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<GameDbContext>();
        context.MigrateAndSeed();
    }

    public static void MigrateAndSeed(this GameDbContext context)
    {
        context.Database.Migrate();
        context.Choices.ExecuteDelete();
        context.AddRange(Rock, Paper, Scissors, Spock, Lizard);
        context.SaveChanges();
        var createdLizard = context.Choices.Single(choice => choice.Name == Lizard.Name);
        var createdPaper = context.Choices.Single(choice => choice.Name == Paper.Name);
        var createdSpock = context.Choices.Single(choice => choice.Name == Spock.Name);
        var createdRock = context.Choices.Single(choice => choice.Name == Rock.Name);
        var createdScissors = context.Choices.Single(choice => choice.Name == Scissors.Name);

        createdLizard.WeakerChoices = [createdPaper, createdSpock];
        createdPaper.WeakerChoices = [createdRock, createdSpock];
        createdSpock.WeakerChoices = [createdScissors, createdRock];
        createdRock.WeakerChoices = [createdLizard, createdScissors];
        createdScissors.WeakerChoices = [createdPaper, createdLizard];

        context.SaveChangesAsync();

    }
}
