using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IUserService
    {
        UserDto Register(UserRegisterDto request);
        string Login(UserLoginDto request);
        void DepositToAccount(Guid userId, float amount);
        float GetAccountBalance(Guid userId);
        bool WithdrawFromAccount(Guid userId, float amount);

    }
}
