namespace CadeMeuPet.Domain.Entities;

public sealed class PetReport
{
    private PetReport(
        Guid id,
        string petName,
        string city,
        PetReportStatus status,
        DateTimeOffset reportedAtUtc)
    {
        Id = id;
        PetName = petName;
        City = city;
        Status = status;
        ReportedAtUtc = reportedAtUtc;
    }

    public Guid Id { get; }

    public string PetName { get; }

    public string City { get; }

    public PetReportStatus Status { get; }

    public DateTimeOffset ReportedAtUtc { get; }

    public static PetReport CreateLostPetReport(
        string petName,
        string city,
        DateTimeOffset reportedAtUtc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(petName);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);

        return new PetReport(Guid.NewGuid(), petName, city, PetReportStatus.Lost, reportedAtUtc);
    }
}
