using System.Security.Claims;
using CadeMeuPet.Infrastructure.Tenancy;

namespace CadeMeuPet.Api.Extensions;

public static class ClaimsPrincipalTenantExtensions
{
    public static Guid? GetTenantId(this ClaimsPrincipal user)
    {
        return TenantIdResolver.ResolveTenantId(user);
    }
}
