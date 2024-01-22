using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class Order
    {
        public Order()
        {
            UserDetails = new UserDetails();
            OrderDetails = new List<OrderDetails>();
        }

        public Order(Guid id, Guid userDetailsId, UserDetails userDetails, float total, OrderStatus orderStatus, DeliveryType deliveryType, PaymentType paymentType)
        {
            Id = id;
            UserDetailsId = userDetailsId;
            UserDetails = userDetails;
            CreatedDate= DateTime.Now;
            OrderStatus = orderStatus;
            DeliveryType = deliveryType;
            PaymentType = paymentType;  
            Total = total;
            
        }
       
        public Guid Id { get; set; }
        public Guid UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public PaymentType PaymentType { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Order order &&
                   Id.Equals(order.Id) &&
                   EqualityComparer<UserDetails>.Default.Equals(UserDetails, order.UserDetails) &&
                   EqualityComparer<ICollection<OrderDetails>>.Default.Equals(OrderDetails, order.OrderDetails) &&
                   CreatedDate == order.CreatedDate &&
                   Total == order.Total &&
                   OrderStatus == order.OrderStatus;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserDetails, OrderDetails, CreatedDate, Total, OrderStatus);
        }
    }
}
