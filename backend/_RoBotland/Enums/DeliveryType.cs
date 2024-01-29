using System.ComponentModel;

namespace _RoBotland.Enums
{
    public enum DeliveryType
    {
        [Description("Paczkomat")]
        A,
        [Description("Kurier InPost")]
        B,
        [Description("Odbiór własny")]
        C
    }
}
