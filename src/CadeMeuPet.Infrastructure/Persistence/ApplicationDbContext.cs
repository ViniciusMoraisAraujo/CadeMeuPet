using CadeMeuPet.Application.Common.Tenancy;
using CadeMeuPet.Domain.Common;
using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CadeMeuPet.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public Guid CurrentTenantId => _tenantProvider.TenantId;

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<QrCode> QrCodes => Set<QrCode>();
    public DbSet<ScanHistory> ScanHistories => Set<ScanHistory>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pet>().HasQueryFilter(entity => entity.TenantId == CurrentTenantId);
        modelBuilder.Entity<QrCode>().HasQueryFilter(entity => entity.TenantId == CurrentTenantId);
        modelBuilder.Entity<ScanHistory>().HasQueryFilter(entity => entity.TenantId == CurrentTenantId);
        modelBuilder.Entity<Notification>().HasQueryFilter(entity => entity.TenantId == CurrentTenantId);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(ImplementsTenantScopedEntity))
        {
            modelBuilder.Entity(entityType.ClrType).HasIndex(nameof(ITenantScopedEntity.TenantId));
            modelBuilder.Entity(entityType.ClrType).Property<Guid>(nameof(ITenantScopedEntity.TenantId)).IsRequired();
        }

        modelBuilder.Entity<QrCode>()
            .HasOne(qrCode => qrCode.Pet)
            .WithMany()
            .HasForeignKey(qrCode => qrCode.PetId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScanHistory>()
            .HasOne(scanHistory => scanHistory.QrCode)
            .WithMany()
            .HasForeignKey(scanHistory => scanHistory.QrCodeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
            .HasOne(notification => notification.Pet)
            .WithMany()
            .HasForeignKey(notification => notification.PetId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public override int SaveChanges()
    {
        ApplyTenantToTrackedEntities();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyTenantToTrackedEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTenantToTrackedEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyTenantToTrackedEntities();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private static bool ImplementsTenantScopedEntity(IMutableEntityType entityType)
    {
        return typeof(ITenantScopedEntity).IsAssignableFrom(entityType.ClrType);
    }

    private void ApplyTenantToTrackedEntities()
    {
        foreach (var entry in ChangeTracker.Entries<ITenantScopedEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                SetTenantOnAddedEntity(entry);
                continue;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(entity => entity.TenantId).IsModified = false;
            }
        }
    }

    private void SetTenantOnAddedEntity(EntityEntry<ITenantScopedEntity> entry)
    {
        var currentTenantId = CurrentTenantId;

        if (entry.Entity.TenantId == Guid.Empty)
        {
            entry.Entity.TenantId = currentTenantId;
            return;
        }

        if (entry.Entity.TenantId != currentTenantId)
        {
            throw new InvalidOperationException("Cannot create an entity for a tenant different from the current tenant.");
        }
    }
}
