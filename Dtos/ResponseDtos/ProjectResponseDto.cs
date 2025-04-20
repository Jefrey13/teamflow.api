namespace teamflow.API.Dtos.ResponseDtos
{
    public class ProjectResponseDto
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
