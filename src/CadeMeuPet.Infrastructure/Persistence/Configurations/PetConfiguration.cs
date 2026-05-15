using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadeMeuPet.Infrastructure.Persistence.Configurations;

public sealed class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");

        builder.HasKey(pet => pet.Id);

        builder.Property(pet => pet.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(pet => pet.Species)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(pet => pet.Breed)
            .HasMaxLength(120);

        builder.Property(pet => pet.Color)
            .HasMaxLength(80);

        builder.Property(pet => pet.Description)
            .HasMaxLength(500);

        builder.Property(pet => pet.CreatedAt)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(pet => pet.Tutor)
            .WithMany(tutor => tutor.Pets)
            .HasForeignKey(pet => pet.TutorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
