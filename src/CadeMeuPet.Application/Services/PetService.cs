using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

internal sealed class PetService(IPetRepository petRepository) : IPetService
{
    public Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return petRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<IReadOnlyList<Pet>> ListByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default)
    {
        return petRepository.ListByTutorAsync(tutorId, cancellationToken);
    }

    public Task AddAsync(Pet pet, CancellationToken cancellationToken = default)
    {
        return petRepository.AddAsync(pet, cancellationToken);
    }
}
