using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class ProjectMemberRepository : BaseRepository<ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(AppDbContext ctx)
            : base(ctx)
        { }

        public async Task<IEnumerable<ProjectMember>> GetByProjectAsync(Guid projectId) =>
            await _context.ProjectMembers
            .Where(pm => pm.ProjectId == projectId && pm.IsActive)
            .ToListAsync();

        public async Task<IEnumerable<ProjectMember>> GetByUserAsync(Guid userId) =>
            await _context.ProjectMembers
                .Where(pm => pm.UserId == userId && pm.IsActive)
                .ToListAsync();

        public override async Task RemoveAsync(Guid id)
        {
            var member = await _context.ProjectMembers.FindAsync(id);
            if (member == null) return;

            member.IsActive = false;
            member.RemovedAt = DateTime.UtcNow;
            _context.ProjectMembers.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
