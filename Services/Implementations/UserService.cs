using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using teamflow.API.Dtos.RequestDtos;
using teamflow.API.Dtos.ResponseDtos;
using teamflow.API.Models;
using teamflow.API.Repositories.Interfaces;
using teamflow.API.Services.Interfaces;

namespace teamflow.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _config;

        public UserService(
            IUserRepository userRepo,
            IRoleRepository roleRepo,
            IUserRoleRepository userRoleRepo,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IConfiguration config)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _userRoleRepo = userRoleRepo;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _config = config;
        }

        public async Task<AuthResponseDto> AuthenticateAsync(UserLoginRequestDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.Username)
                       ?? throw new ApplicationException("Usuario o contraseña inválidos.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result != PasswordVerificationResult.Success)
                throw new ApplicationException("Usuario o contraseña inválidos.");

            var roles = (await _userRoleRepo.GetByUserAsync(user.UserId))
                        .Select(ur => ur.Role.RoleName);

            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresAt = tokenDescriptor.Expires.Value
            };
        }



        public async Task DeleteAsync(Guid id)
        {
            if(!await _userRepo.ExistsAsync(id))
                throw new KeyNotFoundException("Usuario no encontrado.");

            await _userRepo.RemoveAsync(id);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Usuario no encontrado.");
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<AuthResponseDto> RegisterAsync(UserRegisterRequestDto dto)
        {
            if (await _userRepo.GetByUsernameAsync(dto.Username) != null)
                throw new ApplicationException("El usuario ya existe.");

            if ((await _userRepo.GetAllAsync()).Any(u => u.Email == dto.Email))
                throw new ApplicationException("El email ya está registrado.");

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            await _userRepo.AddAsync(user);

            var role = await _roleRepo.GetByNameAsync("User");
            if (role != null)
            {
                await _userRoleRepo.AddAsync(new UserRole
                {
                    UserId = user.UserId,
                    RoleId = role.RoleId,
                    AssignedAt = DateTime.UtcNow
                });
            }

            return await AuthenticateAsync(new UserLoginRequestDto
            {
                Username = dto.Username,
                Password = dto.Password
            });
        }

        public async Task<UserResponseDto> UpdateAsync(Guid id, UserUpdateRequestDto dto)
        {
            var user = await _userRepo.GetByIdAsync(id)
                       ?? throw new KeyNotFoundException("Usuario no encontrado.");

            _mapper.Map(dto, user);

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
                user.PasswordHash =
                    _passwordHasher.HashPassword(user, dto.NewPassword);

            await _userRepo.UpdateAsync(user);
            return _mapper.Map<UserResponseDto>(user);
        }
    }
}
