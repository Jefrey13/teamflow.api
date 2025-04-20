namespace teamflow.API.Dtos.ResponseDtos
{
    public class TeamResponseDto
    {
        public Guid TeamId { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
