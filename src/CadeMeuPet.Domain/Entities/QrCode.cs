using CadeMeuPet.Domain.Common;

namespace CadeMeuPet.Domain.Entities;

public sealed class QrCode : ITenantScopedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid PetId { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public Pet? Pet { get; set; }
}
