using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Api.Controllers;

public sealed record PetSearchStatusResponse(string Message, PetReport SampleReport);
