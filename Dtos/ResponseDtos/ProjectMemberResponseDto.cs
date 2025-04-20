namespace teamflow.API.Dtos.ResponseDtos
{
    public class ProjectMemberResponseDto
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; } = default!;
        public string Role { get; set; } = default!;
        public DateTime AddedAt { get; set; }
    }
}