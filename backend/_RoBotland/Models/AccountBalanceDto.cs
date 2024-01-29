namespace _RoBotland.Models
{
    public class AccountBalanceDto
    {
        public AccountBalanceDto(float accountBalance)
        {
            AccountBalance = accountBalance;
        }

        public float AccountBalance { get; set; }
    }
}
