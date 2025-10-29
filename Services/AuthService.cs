using AutoMapper;
using Backend.DTOs.Auth;
using Backend.DTOs.Common;
using Backend.DTOs.User;
using Backend.Entities;
using Backend.Exceptions;
using Backend.Helpers;
using Backend.Repositories;

namespace Backend.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ApiResponse<object>> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !PasswordHasher.VerifyPassword(user.Password, dto.Password))
                return ApiResponse<object>.Fail(StatusCodes.Status401Unauthorized, "Invalid username or password");

            var secret = _config["Jwt:Secret"];
            return ApiResponse<object>.Ok(new { token = JwtTokenGenerator.GenerateToken(user, secret!) });
        }

        public async Task<ApiResponse<UserDto>> RegisterAsync(RegisterDto dto)
        {
            User? userEmailExist = await _userRepository.GetByEmailAsync(dto.Email);
            if (userEmailExist != null)
            {
                return ApiResponse<UserDto>.Fail(StatusCodes.Status400BadRequest, "Email already in use by another user.");
            }

            User user = _mapper.Map<User>(dto);
            user.Password = PasswordHasher.HashPassword(dto.Password);
            await _userRepository.Create(user);

            return ApiResponse<UserDto>.Ok(_mapper.Map<UserDto>(user));
        }
    }
}
