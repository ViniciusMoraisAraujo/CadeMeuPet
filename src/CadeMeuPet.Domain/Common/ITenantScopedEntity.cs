namespace CadeMeuPet.Domain.Common;

/// <summary>
/// Marks entities whose records must be isolated by the current tenant.
/// </summary>
public interface ITenantScopedEntity
{
    /// <summary>
    /// Identifier of the tutor tenant that owns this record.
    /// </summary>
    Guid TenantId { get; set; }
}
