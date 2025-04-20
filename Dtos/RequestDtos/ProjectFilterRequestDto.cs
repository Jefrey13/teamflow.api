namespace teamflow.API.Dtos.RequestDtos
{
    public class ProjectFilterRequestDto
    {
        public string? SearchText { get; set; }
        public string? Status { get; set; }
        public DateOnly? StartDateFrom { get; set; }
        public DateOnly? EndDateTo { get; set; }
    }
}
