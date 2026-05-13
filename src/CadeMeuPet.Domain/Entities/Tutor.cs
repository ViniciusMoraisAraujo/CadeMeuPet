namespace CadeMeuPet.Domain.Entities;

public sealed class Tutor
{
    private readonly List<Pet> _pets = [];
    private readonly List<Notification> _notifications = [];

    private Tutor()
    {
        Name = string.Empty;
        Email = string.Empty;
        PhoneNumber = string.Empty;
    }

    public Tutor(string name, string email, string phoneNumber, string? whatsappNumber = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        WhatsappNumber = whatsappNumber;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? WhatsappNumber { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public IReadOnlyCollection<Pet> Pets => _pets.AsReadOnly();
    public IReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();

    public Pet RegisterPet(
        string name,
        CadeMeuPet.Domain.Enums.PetSpecies species,
        string? breed = null,
        int? age = null,
        string? photoUrl = null,
        string? medicalInformation = null,
        string? observations = null,
        string? characteristics = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (age is < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(age), "A idade do pet não pode ser negativa.");
        }

        var pet = new Pet(
            this,
            name,
            species,
            breed,
            age,
            photoUrl,
            medicalInformation,
            observations,
            characteristics);

        _pets.Add(pet);
        return pet;
    }

    public void AddNotification(Notification notification)
    {
        ArgumentNullException.ThrowIfNull(notification);

        _notifications.Add(notification);
    }

    public PublicTutorContact ToPublicContact()
    {
        return new PublicTutorContact(Name, PhoneNumber, WhatsappNumber);
    }
}

public sealed record PublicTutorContact(string Name, string PhoneNumber, string? WhatsappNumber);
