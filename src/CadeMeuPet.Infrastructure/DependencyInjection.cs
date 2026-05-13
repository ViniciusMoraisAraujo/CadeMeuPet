using CadeMeuPet.Application.Common.Tenancy;
using CadeMeuPet.Application.Pets;
using CadeMeuPet.Infrastructure.Persistence;
using CadeMeuPet.Infrastructure.Persistence.Repositories;
using CadeMeuPet.Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CadeMeuPet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddHttpContextAccessor();
        services.AddScoped<ITenantProvider, TenantProvider>();
        services.AddScoped<IPetRepository, PetRepository>();

        return services;
    }
}
