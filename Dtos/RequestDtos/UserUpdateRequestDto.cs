namespace teamflow.API.Dtos.RequestDtos
{
    public class UserUpdateRequestDto
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? NewPassword { get; set; }
    }
}