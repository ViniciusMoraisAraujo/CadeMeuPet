using CadeMeuPet.Application.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CadeMeuPet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

        return services;
    }
}
