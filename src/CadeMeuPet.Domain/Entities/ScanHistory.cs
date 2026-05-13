using CadeMeuPet.Domain.Common;

namespace CadeMeuPet.Domain.Entities;

public sealed class ScanHistory : ITenantScopedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid QrCodeId { get; set; }
    public DateTimeOffset ScannedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public QrCode? QrCode { get; set; }
}
