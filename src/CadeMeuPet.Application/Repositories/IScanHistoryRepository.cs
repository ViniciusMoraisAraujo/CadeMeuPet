using CadeMeuPet.Domain.Entities;

namespace CadeMeuPet.Application.Repositories;

public interface IScanHistoryRepository : IRepository<ScanHistory>
{
    Task<IReadOnlyList<ScanHistory>> ListByQrCodeAsync(Guid qrCodeId, CancellationToken cancellationToken = default);
}
