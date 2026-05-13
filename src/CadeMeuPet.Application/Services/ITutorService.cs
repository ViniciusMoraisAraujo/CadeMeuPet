using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

public interface ITutorService
{
    Task<Tutor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Tutor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task AddAsync(Tutor tutor, CancellationToken cancellationToken = default);
}
