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
}
