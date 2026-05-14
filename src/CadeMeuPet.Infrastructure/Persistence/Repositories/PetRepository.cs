using CadeMeuPet.Application.Pets;
using CadeMeuPet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Persistence.Repositories;

public sealed class PetRepository : IPetRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PetRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Pets.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Pet>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Pets
            .OrderBy(pet => pet.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Pet pet, CancellationToken cancellationToken = default)
    {
        await _dbContext.Pets.AddAsync(pet, cancellationToken);
    }
}
