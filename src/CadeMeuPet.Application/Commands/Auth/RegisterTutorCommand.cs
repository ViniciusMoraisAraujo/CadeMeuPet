namespace CadeMeuPet.Application.Commands.Auth;

public sealed record RegisterTutorCommand(
    string? Nome,
    string? Email,
    string? Telefone,
    string? Senha);
