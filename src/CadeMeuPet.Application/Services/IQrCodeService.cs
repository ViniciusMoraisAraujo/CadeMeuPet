using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

public interface IQrCodeService
{
    Task<QrCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<QrCode>> ListActiveByPetAsync(Guid petId, CancellationToken cancellationToken = default);

    Task AddAsync(QrCode qrCode, CancellationToken cancellationToken = default);
}
