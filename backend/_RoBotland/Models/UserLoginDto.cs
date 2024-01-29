using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="To pole jest wymagane!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane!")]
        public string Password { get; set; }
    }
}
