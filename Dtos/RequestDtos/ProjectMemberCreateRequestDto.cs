namespace teamflow.API.Dtos.RequestDtos
{
    public class ProjectMemberCreateRequestDto
    {
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; } = default!;
    }
}
