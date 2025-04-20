using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class ProjectTaskRepository
        : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(AppDbContext ctx)
            : base(ctx)
        { }

        public async Task<IEnumerable<ProjectTask>> GetByProjectAsync(Guid projectId) =>
            await _context.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();

        public async Task<IEnumerable<ProjectTask>> GetFilteredByProjectAsync(
            Guid projectId, string? status, string? priority
        )
        {
            var q = _context.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                q = q.Where(t => t.Status == status);

            if (!string.IsNullOrWhiteSpace(priority))
                q = q.Where(t => t.Priority == priority);

            return await q.ToListAsync();
        }

        public async Task<int> CountByStatusAsync(Guid projectId, string status) =>
            await _context.ProjectTasks
                .CountAsync(t => t.ProjectId == projectId && t.Status == status);

        public async Task<IEnumerable<ProjectTask>> GetRecentAsync(Guid projectId, int limit) =>
            await _context.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.CreatedAt)
                .Take(limit)
                .ToListAsync();

        public override async Task RemoveAsync(Guid id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task is null) return;

            task.Status = "Cancelled";
            task.UpdatedAt = DateTime.UtcNow;
            _context.ProjectTasks.Update(task);
            await _context.SaveChangesAsync();
        }
    }
}
