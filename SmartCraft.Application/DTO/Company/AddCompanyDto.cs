using SmartCraft.Application.DTO.Contacts;
using SmartCraft.Core.Entities.Enums;

namespace SmartCraft.Application.DTO.Company;

public record AddCompanyDto(
    string Name,
    string Industry,
    string Website,
    CompanySize Size,
    IEnumerable<AddContactDto> Contacts)
{
    public Core.Entities.Company ToEntity()
    {
        return new Core.Entities.Company
        {
            Name = Name,
            Industry = Industry,
            Website = Website,
            Size = Size,
            Contacts = Contacts.Select(i => i.ToEntity()).ToList()
        };
    }
}