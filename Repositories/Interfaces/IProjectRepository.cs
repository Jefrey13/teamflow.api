using teamflow.API.Dtos.RequestDtos;
using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IProjectRepository: IBaseRepository<Project>
    {
        Task<IEnumerable<Project>> GetFilteredAsync(
            string? searchText,
            string? status,
            DateRange? dateRange
        );
    }
}
