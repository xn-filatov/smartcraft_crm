using SmartCraft.Application.DTO.Interactions;
using SmartCraft.Core.Entities;

namespace SmartCraft.Application.DTO.Contacts;

public record AddContactDto(
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    string JobTitle,
    DateTime? LastContacted,
    IEnumerable<AddInteractionDto> Interactions)

{
    public Contact ToEntity()
    {
        return new Contact
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Phone = Phone,
            JobTitle = JobTitle,
            LastContacted = LastContacted,
            Interactions = Interactions.Select(i => i.ToEntity()).ToList()
        };
    }
}