using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

public interface IPetService
{
    Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Pet>> ListByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default);

    Task AddAsync(Pet pet, CancellationToken cancellationToken = default);
}
