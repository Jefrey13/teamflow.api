using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Dtos.RequestDtos;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context): base(context) { }

        public async Task<IEnumerable<Project>> GetFilteredAsync(
            string? searchText,
            string? status,
            DateRange? dateRange
        )
        {
            var q = _context.Projects.AsQueryable();

            // excluir “eliminados”
            q = q.Where(p => p.Status != "Cancelled");

            if (!string.IsNullOrWhiteSpace(searchText))
                q = q.Where(p => p.Title.Contains(searchText));

            if (!string.IsNullOrWhiteSpace(status) && status != "All")
                q = q.Where(p => p.Status == status);

            if (dateRange is not null)
            {
                // ateTime del DTO a DateOnly
                var from = DateOnly.FromDateTime(dateRange.From);
                var to = DateOnly.FromDateTime(dateRange.To);

                q = q.Where(p =>
                   p.StartDate >= from &&
                       (p.EndDate.HasValue ? p.EndDate.Value : DateOnly.MaxValue) <= to
                );
            }

            return await q.ToListAsync();
        }

        public override async Task RemoveAsync(Guid id)
        {
            var proj = await _context.Projects.FindAsync(id);
            if (proj is null) return;

            proj.Status = "Cancelled";
            proj.UpdatedAt = DateTime.UtcNow;
            _context.Projects.Update(proj);
            await _context.SaveChangesAsync();
        }
    }
}