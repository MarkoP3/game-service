using GameService.Application.Abstractions;
using GameService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure.Repositories;

public class ChoiceRepository(GameDbContext dbContext) 
    : IChoiceRepository
{
    public async Task<IEnumerable<Choice>> GetAllChoicesAsync(CancellationToken cancellationToken)
        => await dbContext.Choices.ToListAsync(cancellationToken);

    public async Task<Choice> GetChoiceByIdAsync(int id, CancellationToken cancellationToken)
        => await dbContext.Choices.Include(choice => choice.WeakerChoices).SingleAsync(choice => choice.Id == id, cancellationToken);

    public async Task<Choice> GetChoiceAtIndexAsync(int index, CancellationToken cancellationToken)
        => await dbContext.Choices.Include(choice => choice.WeakerChoices).ElementAtAsync(index, cancellationToken: cancellationToken);


    public Task<int> GetChoicesCountAsync(CancellationToken cancellationToken)
        => dbContext.Choices.CountAsync(cancellationToken);
}
