using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using teamflow.API.Dtos.RequestDtos;
using teamflow.API.Dtos.ResponseDtos;
using teamflow.API.Helpers;
using teamflow.API.Services.Interfaces;

namespace teamflow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService) =>
            _userService = userService;

        [HttpPost("login")]
        [AllowAnonymous]
        [EnableCors("LoginCors")]
        public async Task<IActionResult> Login(UserLoginRequestDto dto)
        {
            var auth = await _userService.AuthenticateAsync(dto);
            return Ok(new ApiResponse<AuthResponseDto> { Success = true, 
                Data = auth, Message = "Inicio de sesión exitoso." });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterRequestDto dto)
        {
            var auth = await _userService.RegisterAsync(dto);
            return Ok(new ApiResponse<AuthResponseDto> { Success = true, 
                Data = auth, Message = "Registro completado y usuario autenticado." });
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(new ApiResponse<UserResponseDto> { Success = true, 
                Data = user, Message = "Usuario obtenido correctamente."});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var list = await _userService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<UserResponseDto>> { Success = true, 
                Data = list, Message = "Listado de usuarios obtenido correctamente."});
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, UserUpdateRequestDto dto)
        {
            var updated = await _userService.UpdateAsync(id, dto);
            return Ok(new ApiResponse<UserResponseDto> { Success = true, 
                Data = updated, Message = "Usuario actualizado correctamente." });
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteAsync(id);
            return Ok(new ApiResponse<object> { Success = true, 
                Message = "Usuario eliminado correctamente." });
        }
    }
}
