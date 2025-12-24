using SmartCraft.Application.DTO.Company;
using SmartCraft.Application.DTO.Contacts;
using SmartCraft.Application.DTO.Interactions;
using SmartCraft.Core.Entities;

namespace SmartCraft.Application.DTO;

public static class Extensions
{
    public static CompanyDto ToDto(this Core.Entities.Company company)
    {
        return new CompanyDto(
            company.Id,
            company.Name,
            company.Industry,
            company.Website,
            company.Size,
            company.Contacts.Select(c => c.ToDto()));
    }


    public static ContactDto ToDto(this Contact contact)
    {
        return new ContactDto(contact.FullName,
            contact.Email,
            contact.Phone,
            contact.JobTitle,
            contact.Interactions.Select(x => x.ToDto()));
    }

    public static InteractionDto ToDto(this Interaction interaction)
    {
        return new InteractionDto(
            interaction.Type,
            interaction.Notes,
            interaction.Subject,
            interaction.Outcome,
            interaction.Location);
    }
}