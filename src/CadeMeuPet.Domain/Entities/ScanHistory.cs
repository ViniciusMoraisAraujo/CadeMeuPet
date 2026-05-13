namespace CadeMeuPet.Domain.Entities;

public sealed class ScanHistory
{
    private ScanHistory()
    {
        Pet = null!;
    }

    internal ScanHistory(Pet pet, string? approximateIpAddress, string? userAgent)
    {
        ArgumentNullException.ThrowIfNull(pet);

        Id = Guid.NewGuid();
        PetId = pet.Id;
        Pet = pet;
        ScannedAtUtc = DateTimeOffset.UtcNow;
        ApproximateIpAddress = approximateIpAddress;
        UserAgent = userAgent;
    }

    public Guid Id { get; private set; }
    public Guid PetId { get; private set; }
    public Pet Pet { get; private set; }
    public DateTimeOffset ScannedAtUtc { get; private set; }
    public string? ApproximateIpAddress { get; private set; }
    public string? UserAgent { get; private set; }
}
