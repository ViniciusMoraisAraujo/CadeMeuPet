using CadeMeuPet.Domain.Common;

namespace CadeMeuPet.Domain.Entities;

public sealed class Notification : ITenantScopedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid? PetId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Pet? Pet { get; set; }
}
