namespace _RoBotland.Models
{
    public class Order
    {
        public Order()
        {
            UserDetails = new User();
            OrderDetails = new List<OrderDetail>();
        }

        public Order(Guid id, User userDetails, List<OrderDetail> orderDetails)
        {
            Id = id;
            UserDetails = userDetails;
            OrderDetails = orderDetails;
        }
       
        public Guid Id { get; set; }
        public User UserDetails { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus;

        public override bool Equals(object? obj)
        {
            return obj is Order order &&
                   Id.Equals(order.Id) &&
                   EqualityComparer<User>.Default.Equals(UserDetails, order.UserDetails) &&
                   EqualityComparer<ICollection<OrderDetail>>.Default.Equals(OrderDetails, order.OrderDetails) &&
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
