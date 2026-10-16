using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Models
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username wajib diisi.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password wajib diisi.")]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter.")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


}
