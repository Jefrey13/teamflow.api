using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context)
            : base(context)
        { }

        public async Task<Team?> GetByNameAsync(string name) =>
            await _context.Teams
                          .FirstOrDefaultAsync(t => t.Name == name);

        public override async Task RemoveAsync(Guid id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return;

            team.IsActive = false;
            team.RemovedAt = DateTime.UtcNow;
            team.UpdatedAt = DateTime.UtcNow;

            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

    }
}
