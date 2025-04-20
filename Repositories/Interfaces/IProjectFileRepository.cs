using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IProjectFileRepository : IBaseRepository<ProjectFile>
    {
        Task<IEnumerable<ProjectFile>> GetByProjectAsync(Guid projectId);
        Task<IEnumerable<ProjectFile>> GetRecentAsync(Guid projectId, int limit);
    }
}
