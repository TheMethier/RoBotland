namespace _RoBotland.Models;

public class OrderDetail
{
    public OrderDetail()
    {
        Quantity = "";
        Product = new Product();
    }

    public OrderDetail(Guid id, string quantity, float total, Product product)
    {
        Id = id;
        Quantity = quantity;
        Total = total;
        Product = product;
    }

    public Guid Id { get; }
    public string Quantity { get; }
    public float Total { get; }
    public Product Product { get; }

    public override bool Equals(object? obj)
    {
        return obj is OrderDetail detail &&
               Id.Equals(detail.Id) &&
               Quantity == detail.Quantity &&
               Math.Abs(Total - detail.Total) < 0.0001 &&
               EqualityComparer<Product>.Default.Equals(Product, detail.Product);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Quantity, Total, Product);
    }
}