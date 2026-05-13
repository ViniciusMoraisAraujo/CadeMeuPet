namespace CadeMeuPet.Application.Commands.Pets;

public sealed record UpdatePetCommand(
    Guid PetId,
    string? Nome,
    string? Especie,
    string? Raca,
    int? Idade,
    string? FotoUrl,
    string? Observacoes);
