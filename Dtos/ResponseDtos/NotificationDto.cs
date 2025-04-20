namespace teamflow.API.Dtos.ResponseDtos
{
    public class NotificationDto
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public string Message { get; set; } = default!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
