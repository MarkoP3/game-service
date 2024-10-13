using GameService.Domain.Entities;

namespace GameService.Application.Abstractions;

public interface IChoiceRepository
{
    Task<IEnumerable<Choice>> GetAllChoicesAsync(CancellationToken cancellationToken);
    Task<Choice?> GetChoiceByIdAsync(int id, CancellationToken cancellationToken);
    Task<Choice?> GetChoiceAtIndexAsync(int index, CancellationToken cancellationToken);
    Task<int> GetChoicesCountAsync(CancellationToken cancellationToken);
}
