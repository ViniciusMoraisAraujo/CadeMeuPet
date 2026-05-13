namespace CadeMeuPet.Domain.Entities;

public sealed class QrCode
{
    private QrCode()
    {
        PublicCode = string.Empty;
        DestinationUrl = string.Empty;
        Pet = null!;
    }

    internal QrCode(Pet pet, string destinationUrl)
    {
        ArgumentNullException.ThrowIfNull(pet);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationUrl);

        Id = Guid.NewGuid();
        PetId = pet.Id;
        Pet = pet;
        PublicCode = PublicIdentifier.Create();
        DestinationUrl = destinationUrl;
        CreatedAtUtc = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    public Guid Id { get; private set; }
    public Guid PetId { get; private set; }
    public Pet Pet { get; private set; }
    public string PublicCode { get; private set; }
    public string DestinationUrl { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public bool IsActive { get; private set; }

    public void Deactivate()
    {
        IsActive = false;
    }
}
