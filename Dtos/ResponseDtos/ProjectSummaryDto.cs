namespace teamflow.API.Dtos.ResponseDtos
{
    public class ProjectSummaryDto
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public decimal ProgressPercent { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
    }
}
