using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

internal sealed class ScanHistoryService(IScanHistoryRepository scanHistoryRepository) : IScanHistoryService
{
    public Task<IReadOnlyList<ScanHistory>> ListByQrCodeAsync(Guid qrCodeId, CancellationToken cancellationToken = default)
    {
        return scanHistoryRepository.ListByQrCodeAsync(qrCodeId, cancellationToken);
    }

    public Task AddAsync(ScanHistory scanHistory, CancellationToken cancellationToken = default)
    {
        return scanHistoryRepository.AddAsync(scanHistory, cancellationToken);
    }
}
