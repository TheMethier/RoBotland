using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IUserService
    {
        UserDto Register(UserRegisterDto request);
        string Login(UserLoginDto request);

    }
}
