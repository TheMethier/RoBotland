using _RoBotland.Enums;
using System.ComponentModel.DataAnnotations;

namespace _RoBotland.Models
{
    public class OrderOptionsDto
    {
        [Required]
        public DeliveryType DeliveryType { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
    }
}
