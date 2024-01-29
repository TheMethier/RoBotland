using System.ComponentModel;

namespace _RoBotland.Enums
{
    public enum PaymentType
    {
        [Description("RoWallet")]
        A,
        [Description("Płatność przy odbiorze")]
        B,
        [Description("Blik")]
        C,
    }
}
