using Asp.Versioning.Builder;
using GameService.Application.Game.v1.Commands;
using GameService.Application.Game.v1.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace GameService.Host.v1;

public static class GameModule
{
    public static IEndpointRouteBuilder AddGameEndpointsV1(this IEndpointRouteBuilder routeBuilder, ApiVersionSet apiVersionSet)
    {
        routeBuilder.MapGet("/choices", ([FromServices] ISender sender) => sender.Send(new GetChoicesQuery()))
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi();

        routeBuilder.MapGet("/choice", ([FromServices] ISender sender) => sender.Send(new GetRandomChoiceQuery()))
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi();

        routeBuilder.MapPost("/play", ([FromBody] PlayGameCommand command, [FromServices] ISender sender) => sender.Send(command))
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1.0)
            .WithOpenApi();

        return routeBuilder;
    }
}
