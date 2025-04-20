namespace teamflow.API.Dtos.ResponseDtos
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
