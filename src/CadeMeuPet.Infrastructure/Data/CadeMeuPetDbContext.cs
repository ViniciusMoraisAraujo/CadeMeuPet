using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Data;

public sealed class CadeMeuPetDbContext(DbContextOptions<CadeMeuPetDbContext> options) : DbContext(options)
{
    public DbSet<Pet> Pets => Set<Pet>();

    public DbSet<Tutor> Tutors => Set<Tutor>();

    public DbSet<QrCode> QrCodes => Set<QrCode>();

    public DbSet<ScanHistory> ScanHistories => Set<ScanHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tutor>(builder =>
        {
            builder.HasKey(tutor => tutor.Id);
            builder.HasIndex(tutor => new { tutor.TenantId, tutor.Email }).IsUnique();
            builder.Property(tutor => tutor.Name).HasMaxLength(200).IsRequired();
            builder.Property(tutor => tutor.Email).HasMaxLength(320).IsRequired();
        });

        modelBuilder.Entity<Pet>(builder =>
        {
            builder.HasKey(pet => pet.Id);
            builder.HasIndex(pet => new { pet.TenantId, pet.TutorId });
            builder.Property(pet => pet.Name).HasMaxLength(200).IsRequired();
            builder.Property(pet => pet.Species).HasMaxLength(120).IsRequired();
            builder.HasOne(pet => pet.Tutor)
                .WithMany(tutor => tutor.Pets)
                .HasForeignKey(pet => pet.TutorId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QrCode>(builder =>
        {
            builder.HasKey(qrCode => qrCode.Id);
            builder.HasIndex(qrCode => new { qrCode.TenantId, qrCode.Code }).IsUnique();
            builder.HasIndex(qrCode => new { qrCode.TenantId, qrCode.PetId, qrCode.IsActive });
            builder.Property(qrCode => qrCode.Code).HasMaxLength(200).IsRequired();
            builder.HasOne(qrCode => qrCode.Pet)
                .WithMany(pet => pet.QrCodes)
                .HasForeignKey(qrCode => qrCode.PetId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ScanHistory>(builder =>
        {
            builder.HasKey(scanHistory => scanHistory.Id);
            builder.HasIndex(scanHistory => new { scanHistory.TenantId, scanHistory.QrCodeId, scanHistory.ScannedAtUtc });
            builder.Property(scanHistory => scanHistory.IpAddress).HasMaxLength(64);
            builder.Property(scanHistory => scanHistory.UserAgent).HasMaxLength(512);
            builder.HasOne(scanHistory => scanHistory.QrCode)
                .WithMany(qrCode => qrCode.Scans)
                .HasForeignKey(scanHistory => scanHistory.QrCodeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
