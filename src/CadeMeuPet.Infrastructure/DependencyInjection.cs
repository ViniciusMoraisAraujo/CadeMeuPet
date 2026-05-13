using CadeMeuPet.Application.Common.Tenancy;
using CadeMeuPet.Application.Pets;
using CadeMeuPet.Infrastructure.Persistence.Repositories;
using CadeMeuPet.Infrastructure.Tenancy;
using Microsoft.Extensions.DependencyInjection;

namespace CadeMeuPet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ITenantProvider, TenantProvider>();
        services.AddScoped<IPetRepository, PetRepository>();

        return services;
    }
}
