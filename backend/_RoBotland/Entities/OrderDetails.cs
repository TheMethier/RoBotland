namespace _RoBotland.Models;

public class OrderDetails
{
    public OrderDetails()
    {
        Product = new ProductDto();
        Order = new Order();
        Total = 0;
        Quantity = 0;
    }

    public OrderDetails(Guid id, int quantity, float total, ProductDto product,Order order)
    {
        Id = id;
        Quantity = quantity;
        Total = total;
        Product = product;
        Order = order;
    }

    public Guid Id { get; }
    public int Quantity { get; }
    public float Total { get; }
    public int ProductId;
    public Guid OrderId;
    public ProductDto Product { get; }
    public Order Order { get; }


  
}