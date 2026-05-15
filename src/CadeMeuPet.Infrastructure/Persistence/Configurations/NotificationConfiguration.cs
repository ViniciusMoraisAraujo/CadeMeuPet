using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadeMeuPet.Infrastructure.Persistence.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notifications");

        builder.HasKey(notification => notification.Id);

        builder.Property(notification => notification.Channel)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(notification => notification.Message)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(notification => notification.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(notification => notification.Tutor)
            .WithMany(tutor => tutor.Notifications)
            .HasForeignKey(notification => notification.TutorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(notification => notification.ScanHistory)
            .WithMany()
            .HasForeignKey(notification => notification.ScanHistoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
