using GameService.Application.Abstractions;
using GameService.Application.Dtos;
using System.Text.Json.Serialization;

namespace GameService.Application.Game.v1.Commands;

public sealed record PlayGameCommand([property: JsonPropertyName("Player")]int ChoiceId) : ICommand<GameResultResponse>;
