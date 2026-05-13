using CadeMeuPet.Domain.Enums;

namespace CadeMeuPet.Domain.Entities;

public sealed class Notification
{
    private Notification()
    {
        Tutor = null!;
        Title = string.Empty;
        Message = string.Empty;
    }

    public Notification(Tutor tutor, NotificationType type, string title, string message, Guid? petId = null)
    {
        ArgumentNullException.ThrowIfNull(tutor);
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        Id = Guid.NewGuid();
        TutorId = tutor.Id;
        Tutor = tutor;
        PetId = petId;
        Type = type;
        Title = title;
        Message = message;
        CreatedAtUtc = DateTimeOffset.UtcNow;
        IsRead = false;
    }

    public Guid Id { get; private set; }
    public Guid TutorId { get; private set; }
    public Tutor Tutor { get; private set; }
    public Guid? PetId { get; private set; }
    public NotificationType Type { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }
    public DateTimeOffset? ReadAtUtc { get; private set; }
    public bool IsRead { get; private set; }

    public void MarkAsRead()
    {
        IsRead = true;
        ReadAtUtc = DateTimeOffset.UtcNow;
    }
}
