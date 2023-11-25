using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class UserLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
