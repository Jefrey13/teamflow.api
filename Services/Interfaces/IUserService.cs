using teamflow.API.Dtos.RequestDtos;
using teamflow.API.Dtos.ResponseDtos;

namespace teamflow.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDto> AuthenticateAsync(UserLoginRequestDto dto);
        Task<AuthResponseDto> RegisterAsync(UserRegisterRequestDto dto);
        Task<UserResponseDto> UpdateAsync(Guid id, UserUpdateRequestDto dto);
        Task<UserResponseDto> GetByIdAsync(Guid id);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}
