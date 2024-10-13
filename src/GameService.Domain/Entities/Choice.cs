using GameService.Domain.Enums;
using GameService.Domain.Primatives;

namespace GameService.Domain.Entities;

public class Choice : Entity
{
    public required string Name { get; set; }

    public required IEnumerable<Choice> WeakerChoices { get; set; }

    public int Compare(int choiceId) => choiceId switch
    {
        { } when choiceId == Id => 0,
        { } when WeakerChoices.Any(choice => choice.Id == choiceId) => 1,
        _ => -1
    };
}
