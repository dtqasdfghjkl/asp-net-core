using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Role { get; set; }
    }
}
