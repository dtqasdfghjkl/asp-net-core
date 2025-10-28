using AutoMapper;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;
using Backend.DTOs.User;
using Backend.Entities;
using Backend.Helpers;
using Backend.Repositories;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
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

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return false;

            await _userRepository.Delete(user);
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetById(id);
            return user == null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            if (!await _userRepository.IsExists("id", id)) return false;

            User? userEmailExist = await _userRepository.GetByEmailAsync(dto.Email);
            if (userEmailExist != null && userEmailExist.Id != id)
            {
               throw new ValidationException("Email already in use by another user.");
            }
            

            var user = _mapper.Map<User>(dto);
            user.Id = id;
            user.Password = PasswordHasher.HashPassword(dto.Password);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.Update(user);
            return true;
        }
    }
}
