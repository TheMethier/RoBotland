using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class Product
    {
        public Product()
        {
            this.IsAvailable = Availability.A;
            Categories = new List<Category>();
            OrderDetails= new List<OrderDetails>();
            Description = "";
            ImageUrl = "";
            Name = "";
            Id = -1;
        }

        public Product(int id, string name, float price, int quantity, string description, string imageUrl, ICollection<Category> categories, Availability isAvailable, ICollection<OrderDetails>orderDetails)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
            ImageUrl = imageUrl;
            Categories = categories;
            OrderDetails=orderDetails;
            this.IsAvailable = isAvailable;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public Availability IsAvailable { get; set; }

    }
}
