using CadeMeuPet.Domain.Common;

namespace CadeMeuPet.Domain.Entities;

public sealed class Pet : ITenantScopedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid TutorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public QrCode? QrCode { get; set; }
    public ICollection<ScanHistory> ScanHistories { get; set; } = new List<ScanHistory>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
