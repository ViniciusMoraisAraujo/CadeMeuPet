using CadeMeuPet.Application.Common;
using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;
using CadeMeuPet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Repositories;

internal sealed class TutorRepository(CadeMeuPetDbContext dbContext, ICurrentTenant currentTenant)
    : Repository<Tutor>(dbContext, currentTenant), ITutorRepository
{
    public Task<Tutor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return Query.FirstOrDefaultAsync(tutor => tutor.Email == email, cancellationToken);
    }
}
