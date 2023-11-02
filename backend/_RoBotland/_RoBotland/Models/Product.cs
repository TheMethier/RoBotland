namespace _RoBotland.Models
{
    public class Product
    {
        public Product()
        {
            this.IsAvailable = Availability.A;
            Categories = new List<Category>();
            Description = "";
            ImageUrl = "";
            Name = "";
        }

        public Product(int id, string name, float price, int quantity, string description, string imageUrl, ICollection<Category> categories, Availability isAvailable)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
            ImageUrl = imageUrl;
            Categories = categories;
            this.IsAvailable = isAvailable;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Availability IsAvailable { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   Id == product.Id &&
                   Name == product.Name &&
                   Math.Abs(Price - product.Price) < 0.0001 &&
                   Quantity == product.Quantity &&
                   Description == product.Description &&
                   ImageUrl == product.ImageUrl &&
                   EqualityComparer<ICollection<Category>>.Default.Equals(Categories, product.Categories) &&
                   IsAvailable == product.IsAvailable;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Price, Quantity, Description, ImageUrl, Categories, IsAvailable);
        }
    }
}
