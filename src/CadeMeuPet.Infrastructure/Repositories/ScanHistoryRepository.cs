using CadeMeuPet.Application.Common;
using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;
using CadeMeuPet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Repositories;

internal sealed class ScanHistoryRepository(CadeMeuPetDbContext dbContext, ICurrentTenant currentTenant)
    : Repository<ScanHistory>(dbContext, currentTenant), IScanHistoryRepository
{
    public async Task<IReadOnlyList<ScanHistory>> ListByQrCodeAsync(Guid qrCodeId, CancellationToken cancellationToken = default)
    {
        return await Query
            .AsNoTracking()
            .Where(scanHistory => scanHistory.QrCodeId == qrCodeId)
            .OrderByDescending(scanHistory => scanHistory.ScannedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
