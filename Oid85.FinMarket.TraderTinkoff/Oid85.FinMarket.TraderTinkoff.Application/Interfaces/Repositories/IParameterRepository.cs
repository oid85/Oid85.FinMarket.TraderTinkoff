namespace Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories
{
    public interface IParameterRepository
    {
        Task<string?> GetParameterValueAsync(string name);
        Task SetParameterValueAsync(string name, string value);
    }
}
