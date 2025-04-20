using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IProjectTaskRepository : IBaseRepository<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetByProjectAsync(Guid projectId);
        Task<IEnumerable<ProjectTask>> GetFilteredByProjectAsync(
            Guid projectId,
            string? status,
            string? priority
        );
        Task<int> CountByStatusAsync(Guid projectId, string status);
        Task<IEnumerable<ProjectTask>> GetRecentAsync(Guid projectId, int limit);
    }
}
