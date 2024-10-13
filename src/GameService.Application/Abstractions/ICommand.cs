using MediatR;

namespace GameService.Application.Abstractions;

public interface ICommand<out TResponse> 
    : IRequest<TResponse>
{
}
