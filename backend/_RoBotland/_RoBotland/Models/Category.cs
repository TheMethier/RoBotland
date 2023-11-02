namespace _RoBotland.Models
{
    public class Category
    {
        public Category()
        {
            Name = "";
            Products = new List<Product>();
        }

        public Category(int id, string name, ICollection<Product> products)
        {
            Id = id;
            Name = name;
            Products = products;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Category category &&
                   Id == category.Id &&
                   Name == category.Name &&
                   EqualityComparer<ICollection<Product>>.Default.Equals(Products, category.Products);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Products);
        }
    }
}
