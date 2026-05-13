namespace CadeMeuPet.Domain.Entities;

public sealed class Pet : Entity, ITenantEntity
{
    public Guid TenantId { get; set; }

    public Guid TutorId { get; set; }

    public Tutor? Tutor { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Species { get; set; } = string.Empty;

    public ICollection<QrCode> QrCodes { get; set; } = new List<QrCode>();
}
