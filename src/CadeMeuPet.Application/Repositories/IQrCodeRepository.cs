using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Repositories;

public interface IQrCodeRepository : IRepository<QrCode>
{
    Task<QrCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<QrCode>> ListActiveByPetAsync(Guid petId, CancellationToken cancellationToken = default);
}
