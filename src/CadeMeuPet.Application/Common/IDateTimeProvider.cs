namespace CadeMeuPet.Application.Common;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
