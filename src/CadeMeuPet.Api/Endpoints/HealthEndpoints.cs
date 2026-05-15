namespace CadeMeuPet.Api.Endpoints;

public static class HealthEndpoints
{
    public static IEndpointRouteBuilder MapHealthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/health", () => Results.Ok(new
            {
                Status = "Healthy",
                Service = "CadeMeuPet.Api"
            }))
            .WithName("HealthCheck")
            .WithTags("Health");

        return endpoints;
    }
}
