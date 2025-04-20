using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role?> GetByNameAsync(string roleName);
    }
}
