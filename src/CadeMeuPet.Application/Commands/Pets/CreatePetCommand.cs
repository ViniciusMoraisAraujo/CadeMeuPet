namespace CadeMeuPet.Application.Commands.Pets;

public sealed record CreatePetCommand(
    Guid TutorId,
    string? Nome,
    string? Especie,
    string? Raca,
    int? Idade,
    string? FotoUrl,
    string? Observacoes);
