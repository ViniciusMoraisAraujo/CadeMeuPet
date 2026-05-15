using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadeMeuPet.Infrastructure.Persistence.Configurations;

public sealed class QrCodeConfiguration : IEntityTypeConfiguration<QrCode>
{
    public void Configure(EntityTypeBuilder<QrCode> builder)
    {
        builder.ToTable("QrCodes");

        builder.HasKey(qrCode => qrCode.Id);

        builder.Property(qrCode => qrCode.Code)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(qrCode => qrCode.IsActive)
            .HasDefaultValue(true);

        builder.Property(qrCode => qrCode.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(qrCode => qrCode.Code)
            .IsUnique();

        builder.HasOne(qrCode => qrCode.Pet)
            .WithOne(pet => pet.QrCode)
            .HasForeignKey<QrCode>(qrCode => qrCode.PetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
