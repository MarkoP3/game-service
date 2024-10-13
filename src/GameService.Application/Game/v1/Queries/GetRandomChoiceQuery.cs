using GameService.Application.Abstractions;
using GameService.Application.Dtos;

namespace GameService.Application.Game.v1.Queries;

public sealed record GetRandomChoiceQuery() : IQuery<ChoiceResponse>;