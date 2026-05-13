using CadeMeuPet.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CadeMeuPet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<ITutorService, TutorService>();
        services.AddScoped<IQrCodeService, QrCodeService>();
        services.AddScoped<IScanHistoryService, ScanHistoryService>();

        return services;
    }
}
