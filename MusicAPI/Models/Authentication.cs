using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username wajib diisi")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        public string? Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Username wajib diisi")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email wajib diisi")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        public string? Password { get; set; }
    }

    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
