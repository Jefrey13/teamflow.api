using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);
    }
}
