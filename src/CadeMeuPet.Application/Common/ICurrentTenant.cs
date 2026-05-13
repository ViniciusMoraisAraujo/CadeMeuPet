namespace CadeMeuPet.Application.Common;

public interface ICurrentTenant
{
    Guid TenantId { get; }
}
