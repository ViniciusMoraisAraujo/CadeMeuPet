using CadeMeuPet.Api.Filters;
using CadeMeuPet.Api.Middleware;

namespace CadeMeuPet.Api.Extensions;

public static class ValidationExtensions
{
    public static IApplicationBuilder UseValidationErrors(this IApplicationBuilder app) =>
        app.UseMiddleware<ValidationExceptionMiddleware>();

    public static RouteGroupBuilder WithRequestValidation(this RouteGroupBuilder group) =>
        group.AddEndpointFilter<ValidationEndpointFilter>();
}
