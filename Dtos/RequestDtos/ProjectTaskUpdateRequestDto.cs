namespace teamflow.API.Dtos.RequestDtos
{
    public class ProjectTaskUpdateRequestDto
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public Guid? AssignedTo { get; set; }
    }
}
