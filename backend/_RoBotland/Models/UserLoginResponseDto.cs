using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class UserLoginResponseDto
    {
        public UserLoginResponseDto(string token)
        {
            this.Token = token;
        }

        public string Token {  get; set; }
    }
}
