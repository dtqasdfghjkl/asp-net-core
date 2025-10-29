using Backend.DTOs.Auth;
using Backend.DTOs.Common;
using Backend.DTOs.User;

namespace Backend.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> LoginAsync(LoginDto dto);
        Task<ApiResponse<UserDto>> RegisterAsync(RegisterDto dto);
    }
}
