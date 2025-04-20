using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        Task<Team?> GetByNameAsync(string name);
    }
}
