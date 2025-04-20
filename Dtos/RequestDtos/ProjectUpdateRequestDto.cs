namespace teamflow.API.Dtos.RequestDtos
{
    public class ProjectUpdateRequestDto
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public string Status { get; set; } = default!;
    }
}
