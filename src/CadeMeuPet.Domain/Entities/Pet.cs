using CadeMeuPet.Domain.Enums;

namespace CadeMeuPet.Domain.Entities;

public sealed class Pet
{
    private readonly List<QrCode> _qrCodes = [];
    private readonly List<ScanHistory> _scanHistories = [];

    private Pet()
    {
        Name = string.Empty;
        Tutor = null!;
        PublicProfileId = string.Empty;
    }

    internal Pet(
        Tutor tutor,
        string name,
        PetSpecies species,
        string? breed = null,
        int? age = null,
        string? photoUrl = null,
        string? medicalInformation = null,
        string? observations = null,
        string? characteristics = null)
    {
        ArgumentNullException.ThrowIfNull(tutor);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (age is < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(age), "A idade do pet não pode ser negativa.");
        }

        Id = Guid.NewGuid();
        TutorId = tutor.Id;
        Tutor = tutor;
        PublicProfileId = PublicIdentifier.Create();
        Name = name;
        Species = species;
        Breed = breed;
        Age = age;
        PhotoUrl = photoUrl;
        MedicalInformation = medicalInformation;
        Observations = observations;
        Characteristics = characteristics;
        CreatedAtUtc = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid TutorId { get; private set; }
    public Tutor Tutor { get; private set; }
    public string PublicProfileId { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string Name { get; private set; }
    public PetSpecies Species { get; private set; }
    public string? Breed { get; private set; }
    public int? Age { get; private set; }
    public string? MedicalInformation { get; private set; }
    public string? Observations { get; private set; }
    public string? Characteristics { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public IReadOnlyCollection<QrCode> QrCodes => _qrCodes.AsReadOnly();
    public IReadOnlyCollection<ScanHistory> ScanHistories => _scanHistories.AsReadOnly();

    public QrCode CreateQrCode(string destinationUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationUrl);

        var qrCode = new QrCode(this, destinationUrl);
        _qrCodes.Add(qrCode);
        return qrCode;
    }

    public ScanHistory RegisterScan(string? approximateIpAddress, string? userAgent)
    {
        var scanHistory = new ScanHistory(this, approximateIpAddress, userAgent);
        _scanHistories.Add(scanHistory);
        return scanHistory;
    }

    public PublicPetProfile ToPublicProfile()
    {
        return new PublicPetProfile(
            PublicProfileId,
            PhotoUrl,
            Name,
            Species,
            Breed,
            Age,
            MedicalInformation,
            Observations,
            Characteristics,
            Tutor.ToPublicContact());
    }
}

public sealed record PublicPetProfile(
    string PublicProfileId,
    string? PhotoUrl,
    string Name,
    PetSpecies Species,
    string? Breed,
    int? Age,
    string? MedicalInformation,
    string? Observations,
    string? Characteristics,
    PublicTutorContact TutorContact);
