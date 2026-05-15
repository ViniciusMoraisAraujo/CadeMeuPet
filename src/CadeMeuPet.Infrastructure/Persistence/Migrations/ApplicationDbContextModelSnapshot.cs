using System;
using CadeMeuPet.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CadeMeuPet.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ApplicationDbContext))]
partial class ApplicationDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Tutor", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2").HasDefaultValueSql("SYSUTCDATETIME()");
            b.Property<string>("Email").IsRequired().HasMaxLength(256).HasColumnType("nvarchar(256)");
            b.Property<string>("Name").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
            b.Property<string>("PhoneNumber").IsRequired().HasMaxLength(30).HasColumnType("nvarchar(30)");
            b.HasKey("Id");
            b.HasIndex("Email").IsUnique();
            b.ToTable("Tutors");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Pet", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<string>("Breed").HasMaxLength(120).HasColumnType("nvarchar(120)");
            b.Property<string>("Color").HasMaxLength(80).HasColumnType("nvarchar(80)");
            b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2").HasDefaultValueSql("SYSUTCDATETIME()");
            b.Property<string>("Description").HasMaxLength(500).HasColumnType("nvarchar(500)");
            b.Property<string>("Name").IsRequired().HasMaxLength(120).HasColumnType("nvarchar(120)");
            b.Property<string>("Species").IsRequired().HasMaxLength(80).HasColumnType("nvarchar(80)");
            b.Property<Guid>("TutorId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("TutorId");
            b.ToTable("Pets");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.QrCode", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<string>("Code").IsRequired().HasMaxLength(128).HasColumnType("nvarchar(128)");
            b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2").HasDefaultValueSql("SYSUTCDATETIME()");
            b.Property<bool>("IsActive").ValueGeneratedOnAdd().HasColumnType("bit").HasDefaultValue(true);
            b.Property<Guid>("PetId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("Code").IsUnique();
            b.HasIndex("PetId").IsUnique();
            b.ToTable("QrCodes");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.ScanHistory", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<string>("IpAddress").HasMaxLength(45).HasColumnType("nvarchar(45)");
            b.Property<decimal?>("Latitude").HasPrecision(9, 6).HasColumnType("decimal(9,6)");
            b.Property<decimal?>("Longitude").HasPrecision(9, 6).HasColumnType("decimal(9,6)");
            b.Property<Guid>("PetId").HasColumnType("uniqueidentifier");
            b.Property<DateTime>("ScannedAt").ValueGeneratedOnAdd().HasColumnType("datetime2").HasDefaultValueSql("SYSUTCDATETIME()");
            b.Property<string>("UserAgent").HasMaxLength(512).HasColumnType("nvarchar(512)");
            b.HasKey("Id");
            b.HasIndex("PetId");
            b.ToTable("ScanHistories");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Notification", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedOnAdd().HasColumnType("uniqueidentifier");
            b.Property<string>("Channel").IsRequired().HasMaxLength(50).HasColumnType("nvarchar(50)");
            b.Property<DateTime>("CreatedAt").ValueGeneratedOnAdd().HasColumnType("datetime2").HasDefaultValueSql("SYSUTCDATETIME()");
            b.Property<string>("Message").IsRequired().HasMaxLength(1000).HasColumnType("nvarchar(1000)");
            b.Property<Guid?>("ScanHistoryId").HasColumnType("uniqueidentifier");
            b.Property<DateTime?>("SentAt").HasColumnType("datetime2");
            b.Property<Guid>("TutorId").HasColumnType("uniqueidentifier");
            b.HasKey("Id");
            b.HasIndex("ScanHistoryId");
            b.HasIndex("TutorId");
            b.ToTable("Notifications");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Pet", b =>
        {
            b.HasOne("CadeMeuPet.Domain.Entities.Tutor", "Tutor")
                .WithMany("Pets")
                .HasForeignKey("TutorId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.Navigation("Tutor");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.QrCode", b =>
        {
            b.HasOne("CadeMeuPet.Domain.Entities.Pet", "Pet")
                .WithOne("QrCode")
                .HasForeignKey("CadeMeuPet.Domain.Entities.QrCode", "PetId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.Navigation("Pet");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.ScanHistory", b =>
        {
            b.HasOne("CadeMeuPet.Domain.Entities.Pet", "Pet")
                .WithMany("ScanHistories")
                .HasForeignKey("PetId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.Navigation("Pet");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Notification", b =>
        {
            b.HasOne("CadeMeuPet.Domain.Entities.ScanHistory", "ScanHistory")
                .WithMany()
                .HasForeignKey("ScanHistoryId")
                .OnDelete(DeleteBehavior.NoAction);

            b.HasOne("CadeMeuPet.Domain.Entities.Tutor", "Tutor")
                .WithMany("Notifications")
                .HasForeignKey("TutorId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("ScanHistory");
            b.Navigation("Tutor");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Pet", b =>
        {
            b.Navigation("QrCode");
            b.Navigation("ScanHistories");
        });

        modelBuilder.Entity("CadeMeuPet.Domain.Entities.Tutor", b =>
        {
            b.Navigation("Notifications");
            b.Navigation("Pets");
        });
#pragma warning restore 612, 618
    }
}
