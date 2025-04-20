using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetByUserAsync(Guid userId);
        Task<IEnumerable<UserRole>> GetByRoleAsync(byte roleId);
        Task AddAsync(UserRole userRole);
        Task RemoveAsync(Guid userId, byte roleId);
        Task<bool> ExistsAsync(Guid userId, byte roleId);
    }
}
