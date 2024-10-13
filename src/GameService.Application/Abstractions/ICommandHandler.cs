using MediatR;

namespace GameService.Application.Abstractions;

public interface ICommandHandler<TCommand, TResponse> :
    IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}
