using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CadeMeuPet.Infrastructure.Tenancy;

public static class TenantIdResolver
{
    public static Guid? ResolveTenantId(ClaimsPrincipal? user)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var claimValue = user.FindFirstValue(TenantProvider.TenantClaimType)
            ?? user.FindFirstValue(TenantProvider.TutorClaimType)
            ?? user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue("sub");

        return Guid.TryParse(claimValue, out var tenantId) ? tenantId : null;
    }
}
