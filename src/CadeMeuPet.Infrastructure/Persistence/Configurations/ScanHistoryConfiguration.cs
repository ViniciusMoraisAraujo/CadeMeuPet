using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadeMeuPet.Infrastructure.Persistence.Configurations;

public sealed class ScanHistoryConfiguration : IEntityTypeConfiguration<ScanHistory>
{
    public void Configure(EntityTypeBuilder<ScanHistory> builder)
    {
        builder.ToTable("ScanHistories");

        builder.HasKey(scanHistory => scanHistory.Id);

        builder.Property(scanHistory => scanHistory.IpAddress)
            .HasMaxLength(45);

        builder.Property(scanHistory => scanHistory.UserAgent)
            .HasMaxLength(512);

        builder.Property(scanHistory => scanHistory.Latitude)
            .HasPrecision(9, 6);

        builder.Property(scanHistory => scanHistory.Longitude)
            .HasPrecision(9, 6);

        builder.Property(scanHistory => scanHistory.ScannedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(scanHistory => scanHistory.Pet)
            .WithMany(pet => pet.ScanHistories)
            .HasForeignKey(scanHistory => scanHistory.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
