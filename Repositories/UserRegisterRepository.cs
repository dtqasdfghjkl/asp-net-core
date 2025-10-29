using Auth.DTOs.Auth;
using Auth.DTOs.User;
using Auth.Repositories;
using AutoMapper;
using Backend.Data;
using Backend.Entities;
using Backend.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRegisterRepository : BaseRepository<User>, IUserAuthRepository
    {
        private readonly IMapper _mapper;
        public UserRegisterRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<UserAuthDto?> CreateAsync(RegisterDto model, CancellationToken cancellationToken = default)
        {
            User? user = await this.Create(_mapper.Map<User>(model));
            return _mapper.Map<UserAuthDto?>(user);
        }

        public async Task<UserAuthDto?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return _mapper.Map<UserAuthDto?>(user);
        }
    }
}
