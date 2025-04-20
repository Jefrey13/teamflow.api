namespace teamflow.API.Dtos.RequestDtos
{
    public class ProjectTaskCreateRequestDto
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public Guid? AssignedTo { get; set; }
    }
}
