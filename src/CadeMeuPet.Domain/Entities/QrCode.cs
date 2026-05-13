namespace CadeMeuPet.Domain.Entities;

public sealed class QrCode
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public Pet Pet { get; set; } = null!;
}
