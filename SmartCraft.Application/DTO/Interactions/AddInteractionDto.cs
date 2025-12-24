using SmartCraft.Core.Entities;

namespace SmartCraft.Application.DTO.Interactions;

public record AddInteractionDto(
    string Type,
    string Notes,
    string Subject,
    string Outcome,
    string Location)
{
    public Interaction ToEntity()
    {
        return new Interaction()
        {
            Type = Type,
            Notes = Notes,
            Subject = Subject,
            Outcome = Outcome,
            Location = Location
        };
    }
}