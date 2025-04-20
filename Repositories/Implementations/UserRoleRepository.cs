using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _context;
        public UserRoleRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid userId, byte roleId) =>
            await _context.UserRoles
                          .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        public async Task<IEnumerable<UserRole>> GetByUserAsync(Guid userId) =>
            await _context.UserRoles
                          .Include(ur => ur.Role)
                          .Where(ur => ur.UserId == userId)
                          .ToListAsync();

        public async Task<IEnumerable<UserRole>> GetByRoleAsync(byte roleId) =>
            await _context.UserRoles
                          .Include(ur => ur.User)
                          .Where(ur => ur.RoleId == roleId)
                          .ToListAsync();

        public async Task RemoveAsync(Guid userId, byte roleId)
        {
            var ur = await _context.UserRoles.FindAsync(userId, roleId);
            if (ur == null) return;

            ur.IsActive = false;
            ur.RemovedAt = DateTime.UtcNow;

            _context.UserRoles.Update(ur);
            await _context.SaveChangesAsync();
        }
    }
}
