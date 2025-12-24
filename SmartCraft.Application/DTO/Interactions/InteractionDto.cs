namespace SmartCraft.Application.DTO.Interactions;

public record InteractionDto(
    string Type,
    string Notes,
    string Subject,
    string Outcome,
    string Location);