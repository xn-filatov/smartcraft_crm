using SmartCraft.Application.DTO.Contacts;
using SmartCraft.Core.Entities.Enums;

namespace SmartCraft.Application.DTO.Company;

public record CompanyDto(
    int Id,
    string Name,
    string Industry,
    string Website,
    CompanySize Size,
    IEnumerable<ContactDto> Contacts);