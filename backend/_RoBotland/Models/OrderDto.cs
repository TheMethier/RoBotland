using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class OrderDto
    {
        public OrderDto()
        {
        }

        public OrderDto(Guid id, DateTime createdDate, List<ShoppingCartItem> items, UserDetailsDto userDetails, DeliveryType deliveryType, PaymentType paymentType)
        {
            Id = id;
            CreatedDate = createdDate;
            Items = items;
            UserDetails = userDetails;
            DeliveryType = deliveryType;
            PaymentType = paymentType;
        }

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public UserDetailsDto UserDetails { get; set;}
        public DeliveryType DeliveryType {  get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
