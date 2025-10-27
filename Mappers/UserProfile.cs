using AutoMapper;
using WebApplication1.DTOs.Auth;
using WebApplication1.DTOs.User;
using WebApplication1.Entities;

namespace WebApplication1.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
        }
    }
}
