namespace teamflow.API.Dtos.ResponseDtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
    }
}
