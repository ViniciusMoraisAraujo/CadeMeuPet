namespace CadeMeuPet.Domain.Entities;

public sealed class QrCode : Entity, ITenantEntity
{
    public Guid TenantId { get; set; }

    public Guid PetId { get; set; }

    public Pet? Pet { get; set; }

    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public ICollection<ScanHistory> Scans { get; set; } = new List<ScanHistory>();
}
