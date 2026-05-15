using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Services;

public interface IScanHistoryService
{
    Task<IReadOnlyList<ScanHistory>> ListByQrCodeAsync(Guid qrCodeId, CancellationToken cancellationToken = default);

    Task AddAsync(ScanHistory scanHistory, CancellationToken cancellationToken = default);
}
