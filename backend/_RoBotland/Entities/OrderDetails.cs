namespace _RoBotland.Models;

public class OrderDetails
{
    public OrderDetails()
    {
   
        Total = 0;
        Quantity = 0;
    }

    public OrderDetails(Guid id, int quantity, float total, Product product,Order order)
    {
        Id = id;
        Quantity = quantity;
        Total = total;
        Product = product;
        Order = order;
    }

    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public float Total { get; set; }
    public int ProductId { get; set; }
    public Guid OrderId { get; set; }
    public Product Product { get; set; }
    public Order Order { get; set; }


  
}