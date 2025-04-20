namespace teamflow.API.Dtos.ResponseDtos
{
    public class NotificationCountDto
    {
        public Guid UserId { get; set; }
        public int UnreadCount { get; set; }
    }
}
