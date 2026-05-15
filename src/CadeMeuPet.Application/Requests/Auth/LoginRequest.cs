namespace CadeMeuPet.Application.Requests.Auth;

public sealed record LoginRequest(
    string? Email,
    string? Senha);
