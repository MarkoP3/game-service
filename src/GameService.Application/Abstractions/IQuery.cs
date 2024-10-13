using MediatR;

namespace GameService.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
