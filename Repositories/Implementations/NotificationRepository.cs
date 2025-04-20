using Microsoft.EntityFrameworkCore;
using teamflow.API.Data;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;

namespace teamflow.API.Repositories.Implementations
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(AppDbContext ctx)
            : base(ctx)
        { }

        public async Task<IEnumerable<Notification>> GetByUserAsync(Guid userId) =>
            await _context.Notifications
                          .Where(n => n.UserId == userId)
                          .OrderByDescending(n => n.CreatedAt)
                          .ToListAsync();

        public async Task<int> CountUnreadAsync(Guid userId) =>
            await _context.Notifications
                          .CountAsync(n => n.UserId == userId && !n.IsRead);

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notif = await _context.Notifications.FindAsync(notificationId);
            if (notif == null) return;

            notif.IsRead = true;
            notif.UpdatedAt = DateTime.UtcNow;

            _context.Notifications.Update(notif);
            await _context.SaveChangesAsync();
        }
    }
}
