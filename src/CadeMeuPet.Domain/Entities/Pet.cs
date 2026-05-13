namespace CadeMeuPet.Domain.Entities;

public sealed class Pet
{
    public Guid Id { get; set; }
    public Guid TutorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public QrCode? QrCode { get; set; }
    public ICollection<ScanHistory> ScanHistories { get; set; } = new List<ScanHistory>();
}
