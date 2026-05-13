using System.Security.Claims;
using CadeMeuPet.Infrastructure.Tenancy;

namespace CadeMeuPet.Api.Extensions;

public static class ClaimsPrincipalTenantExtensions
{
    public static Guid? GetTenantId(this ClaimsPrincipal user)
    {
        var claimValue = user.FindFirstValue(TenantProvider.TenantClaimType)
            ?? user.FindFirstValue(TenantProvider.TutorClaimType)
            ?? user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue("sub");

        return Guid.TryParse(claimValue, out var tenantId) ? tenantId : null;
    }
}
