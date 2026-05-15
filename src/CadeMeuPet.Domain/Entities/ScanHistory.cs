using CadeMeuPet.Domain.Common;

namespace CadeMeuPet.Domain.Entities;

public sealed class ScanHistory : ITenantScopedEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid PetId { get; set; }
    public Guid? QrCodeId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public DateTimeOffset ScannedAt { get; set; }

    public Pet Pet { get; set; } = null!;
    public QrCode? QrCode { get; set; }
}
