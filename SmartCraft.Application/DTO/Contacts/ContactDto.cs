using SmartCraft.Application.DTO.Interactions;

namespace SmartCraft.Application.DTO.Contacts;

public record ContactDto(
    string FullName,
    string Email,
    string Phone,
    string JobTitle,
    IEnumerable<InteractionDto> Interactions);