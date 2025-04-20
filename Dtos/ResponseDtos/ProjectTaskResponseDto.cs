namespace teamflow.API.Dtos.ResponseDtos
{
    public class ProjectTaskResponseDto
    {
        public Guid TaskId { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string Priority { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string? AssignedUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
