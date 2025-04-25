using System.ComponentModel.DataAnnotations;

namespace JwtAuthApp.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; // This will receive encrypted string
    }
}
