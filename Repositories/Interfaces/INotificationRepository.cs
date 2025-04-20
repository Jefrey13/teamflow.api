using teamflow.API.Models;

namespace teamflow.API.Repositories.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<IEnumerable<Notification>> GetByUserAsync(Guid userId);
        Task<int> CountUnreadAsync(Guid userId);
        Task MarkAsReadAsync(Guid notificationId);
    }
}
