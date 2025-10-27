using AutoMapper;
using WebApplication1.DTOs.Auth;
using WebApplication1.DTOs.User;
using WebApplication1.Entities;
using WebApplication1.Exceptions;
using WebApplication1.Helpers;
using WebApplication1.Repositories;

namespace WebApplication1.Services
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

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null || !PasswordHasher.VerifyPassword(user.Password, dto.Password))
                throw new UnauthorizedAccessException("Invalid username or password");

            var secret = _config["Jwt:Secret"];
            return JwtTokenGenerator.GenerateToken(user, secret!);
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            User? userEmailExist = await _userRepository.GetByEmailAsync(dto.Email);
            if (userEmailExist != null)
            {
                throw new ValidationException("Email already in use by another user.");
            }

            User user = _mapper.Map<User>(dto);
            user.Password = PasswordHasher.HashPassword(dto.Password);
            await _userRepository.Create(user);

            return _mapper.Map<UserDto>(user);
        }
    }
}
