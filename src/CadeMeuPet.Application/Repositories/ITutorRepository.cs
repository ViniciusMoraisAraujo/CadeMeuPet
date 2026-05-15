using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Repositories;

public interface ITutorRepository : IRepository<Tutor>
{
    Task<Tutor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
