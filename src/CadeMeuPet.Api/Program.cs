using CadeMeuPet.Api.Extensions;
using CadeMeuPet.Application;
using CadeMeuPet.Application.Commands.Auth;
using CadeMeuPet.Application.Commands.Pets;
using CadeMeuPet.Application.Requests.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseValidationErrors();

var api = app.MapGroup("/api")
    .WithRequestValidation();

api.MapPost("/tutores", (RegisterTutorCommand command) =>
    Results.Created("/api/tutores", new { message = "Tutor cadastrado com sucesso.", tutor = command }));

api.MapPost("/auth/login", (LoginRequest request) =>
    Results.Ok(new { message = "Login válido.", login = request.Email }));

api.MapPost("/pets", (CreatePetCommand command) =>
    Results.Created("/api/pets", new { message = "Pet cadastrado com sucesso.", pet = command }));

api.MapPut("/pets", (UpdatePetCommand command) =>
    Results.Ok(new { message = "Pet atualizado com sucesso.", pet = command }));

app.Run();

public partial class Program
{
}
