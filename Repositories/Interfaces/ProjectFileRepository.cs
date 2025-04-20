using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Implementations;

namespace teamflow.API.Repositories.Interfaces
{
    public class ProjectFileRepository : BaseRepository<ProjectFile>, IProjectFileRepository
    {
        public ProjectFileRepository(AppDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<ProjectFile>> GetByProjectAsync(Guid projectId) =>
            await _context.ProjectFiles
                          .Where(f => f.ProjectId == projectId && f.IsActive)
                          .ToListAsync();

        public async Task<IEnumerable<ProjectFile>> GetRecentAsync(Guid projectId, int limit) =>
            await _context.ProjectFiles
                          .Where(f => f.ProjectId == projectId && f.IsActive)
                          .OrderByDescending(f => f.UploadedAt)
        .Take(limit)
                          .ToListAsync();

        public override async Task RemoveAsync(Guid id)
        {
            var file = await _context.ProjectFiles.FindAsync(id);
            if (file == null) return;

            file.IsActive = false;
            file.RemovedAt = DateTime.UtcNow;
            _context.ProjectFiles.Update(file);
            await _context.SaveChangesAsync();
        }
    }
}
