using WebApplication1.DTOs.Auth;
using WebApplication1.DTOs.User;

namespace WebApplication1.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<UserDto> RegisterAsync(RegisterDto dto);
    }
}
