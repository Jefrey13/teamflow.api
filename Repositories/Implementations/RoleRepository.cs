using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext ctx)
            : base(ctx)
        { }

        public async Task<Role?> GetByNameAsync(string roleName) =>
            await _context.Roles
                          .FirstOrDefaultAsync(r => r.RoleName == roleName);
    }
}
