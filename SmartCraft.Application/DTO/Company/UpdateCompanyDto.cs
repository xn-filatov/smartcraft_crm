using SmartCraft.Core.Entities.Enums;

namespace SmartCraft.Application.DTO.Company;

public record UpdateCompanyDto(
    int Id,
    string? Name,
    string? Industry,
    string? Website,
    CompanySize? Size);