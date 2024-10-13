using GameService.Domain.Enums;
using System.Text.Json.Serialization;

namespace GameService.Application.Dtos;

public record GameResultResponse([property: JsonConverter(typeof(JsonStringEnumConverter))]GameResult Results, [property: JsonPropertyName("player")] int PlayerChoiceId, [property: JsonPropertyName("computer")] int ComputerChoiceId);
