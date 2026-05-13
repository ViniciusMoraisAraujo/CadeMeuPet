using CadeMeuPet.Application.Common;
using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;
using CadeMeuPet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Repositories;

internal sealed class PetRepository(CadeMeuPetDbContext dbContext, ICurrentTenant currentTenant)
    : Repository<Pet>(dbContext, currentTenant), IPetRepository
{
    public async Task<IReadOnlyList<Pet>> ListByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default)
    {
        return await Query
            .AsNoTracking()
            .Where(pet => pet.TutorId == tutorId)
            .ToListAsync(cancellationToken);
    }
}
