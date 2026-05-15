using CadeMeuPet.Application.Common;
using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;
using CadeMeuPet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Repositories;

internal abstract class Repository<TEntity>(CadeMeuPetDbContext dbContext, ICurrentTenant currentTenant) : IRepository<TEntity>
    where TEntity : Entity, ITenantEntity
{
    protected CadeMeuPetDbContext DbContext { get; } = dbContext;

    protected Guid TenantId { get; } = currentTenant.TenantId;

    protected IQueryable<TEntity> Query => DbContext.Set<TEntity>().Where(entity => entity.TenantId == TenantId);

    public virtual Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Query.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.TenantId = TenantId;
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await EnsureBelongsToTenantAsync(entity.Id, cancellationToken);
        entity.TenantId = TenantId;
        DbContext.Set<TEntity>().Update(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return;
        }

        DbContext.Set<TEntity>().Remove(entity);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    protected async Task EnsureBelongsToTenantAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await Query.AnyAsync(entity => entity.Id == id, cancellationToken);
        if (!exists)
        {
            throw new InvalidOperationException($"Entity '{typeof(TEntity).Name}' with id '{id}' was not found for the current tenant.");
        }
    }
}
