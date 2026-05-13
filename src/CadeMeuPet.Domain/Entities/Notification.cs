using CadeMeuPet.Domain.Common;

namespace CadeMeuPet.Domain.Entities;

public sealed class Notification : ITenantScopedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid TutorId { get; set; }
    public Guid? PetId { get; set; }
    public Guid? ScanHistoryId { get; set; }
    public string? Channel { get; set; }
    public string? Title { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? SentAt { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public Pet? Pet { get; set; }
    public ScanHistory? ScanHistory { get; set; }
}
