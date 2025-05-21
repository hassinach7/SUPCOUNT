namespace SupCountBE.Core.Repositories;

public interface IRoleRepository
{
    Task<List<string?>> GetListAsync();
}
