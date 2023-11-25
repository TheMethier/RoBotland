using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class UserDto
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}
