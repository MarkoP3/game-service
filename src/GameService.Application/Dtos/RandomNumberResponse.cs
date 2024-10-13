using System.Text.Json.Serialization;

namespace GameService.Application.Dtos;

public record RandomNumberResponse(
    [property: JsonPropertyName("random_number")] 
    int Value);
