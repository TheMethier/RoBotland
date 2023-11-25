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

        public Order(Guid id, UserDetails userDetails, List<OrderDetails> orderDetails)
        {
            Id = id;
            UserDetails = userDetails;
            OrderDetails = orderDetails;
        }
       
        public Guid Id { get; set; }
        public Guid UserDetailsId { get; set; }
        public UserDetails UserDetails { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus;

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
