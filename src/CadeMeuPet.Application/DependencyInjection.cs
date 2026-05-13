using CadeMeuPet.Application.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CadeMeuPet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }
}
