using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.DTOs.User
{
    public class UserDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
