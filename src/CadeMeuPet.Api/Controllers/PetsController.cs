using CadeMeuPet.Application.Common;
using CadeMeuPet.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CadeMeuPet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PetsController : ControllerBase
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public PetsController(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    [HttpGet("status")]
    public ActionResult<PetSearchStatusResponse> GetStatus()
    {
        var sampleReport = PetReport.CreateLostPetReport(
            petName: "Ainda não informado",
            city: "Brasil",
            reportedAtUtc: _dateTimeProvider.UtcNow);

        return Ok(new PetSearchStatusResponse(
            Message: "Cadê Meu Pet API pronta para receber funcionalidades.",
            SampleReport: sampleReport));
    }
}
