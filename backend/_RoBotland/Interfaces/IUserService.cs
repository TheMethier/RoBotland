using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IUserService
    {
        UserDto Register(UserRegisterDto request);
        UserLoginResponseDto Login(UserLoginDto request);
        AccountBalanceDto DepositToAccount(string username, float amount);
        AccountBalanceDto GetAccountBalance(string username);
        AccountBalanceDto WithdrawFromAccount(string username, float amount);
        List<OrderDto> GetHistory(string username);
        UserInfoDto GetUserInfo(string username);

    }
}
