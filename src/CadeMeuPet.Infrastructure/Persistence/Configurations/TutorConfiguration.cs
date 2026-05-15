using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadeMeuPet.Infrastructure.Persistence.Configurations;

public sealed class TutorConfiguration : IEntityTypeConfiguration<Tutor>
{
    public void Configure(EntityTypeBuilder<Tutor> builder)
    {
        builder.ToTable("Tutors");

        builder.HasKey(tutor => tutor.Id);

        builder.Property(tutor => tutor.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(tutor => tutor.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(tutor => tutor.PhoneNumber)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(tutor => tutor.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(tutor => tutor.Email)
            .IsUnique();
    }
}
