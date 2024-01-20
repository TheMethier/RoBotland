using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IUserService
    {
        UserDto Register(UserRegisterDto request);
        UserLoginResponseDto Login(UserLoginDto request);
        bool DepositToAccount(string username, float amount);
        float GetAccountBalance(string username);
        bool WithdrawFromAccount(string username, float amount);
        List<OrderDto> GetHistory(string username);
        UserInfoDto GetUserInfo(string username);

    }
}
