using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.User
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Role { get; set; } = 1;
    }
}
