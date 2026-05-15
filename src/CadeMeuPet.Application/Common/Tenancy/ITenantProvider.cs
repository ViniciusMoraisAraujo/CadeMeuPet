namespace CadeMeuPet.Application.Common.Tenancy;

/// <summary>
/// Provides the current tenant. In CadeMeuPet the tenant is the tutor/user that owns the data.
/// </summary>
public interface ITenantProvider
{
    /// <summary>
    /// Tenant identifier resolved from the authenticated user's JWT claims.
    /// </summary>
    Guid TenantId { get; }

    /// <summary>
    /// Returns whether a tenant is available for the current execution context.
    /// </summary>
    bool HasTenant { get; }
}
