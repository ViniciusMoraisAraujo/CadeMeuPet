using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Tutor> Tutors => Set<Tutor>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<QrCode> QrCodes => Set<QrCode>();
    public DbSet<ScanHistory> ScanHistories => Set<ScanHistory>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
