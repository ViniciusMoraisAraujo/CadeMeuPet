using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CadeMeuPet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<ITutorRepository, TutorRepository>();
        services.AddScoped<IQrCodeRepository, QrCodeRepository>();
        services.AddScoped<IScanHistoryRepository, ScanHistoryRepository>();

        return services;
    }
}
