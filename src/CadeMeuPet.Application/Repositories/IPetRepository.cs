using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Repositories;

public interface IPetRepository : IRepository<Pet>
{
    Task<IReadOnlyList<Pet>> ListByTutorAsync(Guid tutorId, CancellationToken cancellationToken = default);
}
