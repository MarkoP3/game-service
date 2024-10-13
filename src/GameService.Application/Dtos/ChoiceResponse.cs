using GameService.Domain.Entities;

namespace GameService.Application.Dtos;

public record ChoiceResponse(int Id, string Name)
{
    public ChoiceResponse(Choice choice) : this(choice.Id, choice.Name) { }
};
