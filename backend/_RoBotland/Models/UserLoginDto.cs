using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class UserLoginDto
    {
        [Required, MinLength(4)]
        public string Username { get; set; }
        [Required, MinLength(5)]
        public string Password { get; set; }
    }
}
