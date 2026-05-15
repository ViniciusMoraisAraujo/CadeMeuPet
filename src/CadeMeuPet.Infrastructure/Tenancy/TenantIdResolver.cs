using System.Security.Claims;

namespace CadeMeuPet.Infrastructure.Tenancy;

public static class TenantIdResolver
{
    public static Guid? ResolveTenantId(ClaimsPrincipal? user)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var claimValue = user.FindFirst(TenantProvider.TenantClaimType)?.Value
            ?? user.FindFirst(TenantProvider.TutorClaimType)?.Value
            ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value;

        return Guid.TryParse(claimValue, out var tenantId) ? tenantId : null;
    }
}
