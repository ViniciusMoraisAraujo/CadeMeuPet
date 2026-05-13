namespace CadeMeuPet.Domain.Entities;

public sealed class Notification
{
    public Guid Id { get; set; }
    public Guid TutorId { get; set; }
    public Guid? ScanHistoryId { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }

    public Tutor Tutor { get; set; } = null!;
    public ScanHistory? ScanHistory { get; set; }
}
