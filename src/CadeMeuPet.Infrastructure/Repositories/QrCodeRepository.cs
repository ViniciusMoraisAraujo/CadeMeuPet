using CadeMeuPet.Application.Common;
using CadeMeuPet.Application.Repositories;
using CadeMeuPet.Domain.Entities;
using CadeMeuPet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CadeMeuPet.Infrastructure.Repositories;

internal sealed class QrCodeRepository(CadeMeuPetDbContext dbContext, ICurrentTenant currentTenant)
    : Repository<QrCode>(dbContext, currentTenant), IQrCodeRepository
{
    public Task<QrCode?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return Query.FirstOrDefaultAsync(qrCode => qrCode.Code == code, cancellationToken);
    }

    public async Task<IReadOnlyList<QrCode>> ListActiveByPetAsync(Guid petId, CancellationToken cancellationToken = default)
    {
        return await Query
            .AsNoTracking()
            .Where(qrCode => qrCode.PetId == petId && qrCode.IsActive)
            .ToListAsync(cancellationToken);
    }
}
