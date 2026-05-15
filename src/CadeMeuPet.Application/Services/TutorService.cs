using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

internal sealed class TutorService(ITutorRepository tutorRepository) : ITutorService
{
    public Task<Tutor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return tutorRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<Tutor?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return tutorRepository.GetByEmailAsync(email, cancellationToken);
    }

    public Task AddAsync(Tutor tutor, CancellationToken cancellationToken = default)
    {
        return tutorRepository.AddAsync(tutor, cancellationToken);
    }
}
