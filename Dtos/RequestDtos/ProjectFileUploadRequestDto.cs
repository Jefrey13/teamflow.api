namespace teamflow.API.Dtos.RequestDtos
{
    public class ProjectFileUploadRequestDto
    {
        public Guid ProjectId { get; set; }
        public IFormFile File { get; set; } = default!;
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? Description { get; set; }
    }
}
