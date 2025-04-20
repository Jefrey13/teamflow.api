using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

        public override async Task RemoveAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                user.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
