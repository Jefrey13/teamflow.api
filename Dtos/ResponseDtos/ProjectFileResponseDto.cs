namespace teamflow.API.Dtos.ResponseDtos
{
    public class ProjectFileResponseDto
    {
        public Guid FileId { get; set; }
        public Guid ProjectId { get; set; }
        public string FileName { get; set; } = default!;
        public string FileType { get; set; } = default!;
        public long FileSize { get; set; }
        public string Url { get; set; } = default!;
        public string UploadedBy { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
    }
}
