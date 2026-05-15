using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

internal sealed class QrCodeService(IQrCodeRepository qrCodeRepository) : IQrCodeService
{
    public Task<QrCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return qrCodeRepository.GetByCodeAsync(code, cancellationToken);
    }

    public Task<IReadOnlyList<QrCode>> ListActiveByPetAsync(Guid petId, CancellationToken cancellationToken = default)
    {
        return qrCodeRepository.ListActiveByPetAsync(petId, cancellationToken);
    }

    public Task AddAsync(QrCode qrCode, CancellationToken cancellationToken = default)
    {
        return qrCodeRepository.AddAsync(qrCode, cancellationToken);
    }
}
