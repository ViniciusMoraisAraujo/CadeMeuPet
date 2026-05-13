using CadeMeuPet.Api.Endpoints;
using CadeMeuPet.Application;
using CadeMeuPet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();
app.MapHealthEndpoints();

app.Run();
