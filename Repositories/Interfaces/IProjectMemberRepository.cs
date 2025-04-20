using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IProjectMemberRepository : IBaseRepository<ProjectMember>
    {
        Task<IEnumerable<ProjectMember>> GetByProjectAsync(Guid projectId);
        Task<IEnumerable<ProjectMember>> GetByUserAsync(Guid userId);
    }
}
