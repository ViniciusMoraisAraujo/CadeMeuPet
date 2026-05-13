namespace CadeMeuPet.Domain.Entities;

public sealed class ScanHistory : Entity, ITenantEntity
{
    public Guid TenantId { get; set; }

    public Guid QrCodeId { get; set; }

    public QrCode? QrCode { get; set; }

    public DateTimeOffset ScannedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }
}
