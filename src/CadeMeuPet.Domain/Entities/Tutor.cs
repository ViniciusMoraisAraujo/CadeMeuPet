namespace CadeMeuPet.Domain.Entities;

public sealed class Tutor : Entity, ITenantEntity
{
    public Guid TenantId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
