namespace GameService.Domain.Exceptions;

public class ChoiceNotFoundException(int? choiceId = null, int? choiceIndex = null) 
    : Exception($"Choice ${(choiceId is not null ? $"with id {choiceId}" : $"at index {choiceIndex}")} not found")
{
}
