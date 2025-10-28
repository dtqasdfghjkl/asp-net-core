using Backend.DTOs.Auth;
using Backend.DTOs.User;

namespace Backend.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<UserDto> RegisterAsync(RegisterDto dto);
    }
}
