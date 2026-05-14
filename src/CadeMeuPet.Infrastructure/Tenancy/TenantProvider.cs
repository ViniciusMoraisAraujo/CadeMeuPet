using CadeMeuPet.Application.Common.Tenancy;
using Microsoft.AspNetCore.Http;

namespace CadeMeuPet.Infrastructure.Tenancy;

public sealed class TenantProvider : ITenantProvider
{
    public const string TenantClaimType = "tenant_id";
    public const string TutorClaimType = "tutor_id";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid TenantId
    {
        get
        {
            var tenantId = ResolveTenantId();

            if (tenantId is null)
            {
                throw new InvalidOperationException("No tenant was found in the authenticated user claims.");
            }

            return tenantId.Value;
        }
    }

    public bool HasTenant => ResolveTenantId().HasValue;

    private Guid? ResolveTenantId()
    {
        return TenantIdResolver.ResolveTenantId(_httpContextAccessor.HttpContext?.User);
    }
}
