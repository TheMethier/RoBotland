namespace _RoBotland.Models
{
    public class Category
    {
        public Category()
        {
            Name = "";
            Products = new List<ProductDto>();
        }

        public Category(int id, string name, ICollection<ProductDto> products)
        {
            Id = id;
            Name = name;
            Products = products;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductDto> Products { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Category category &&
                   Id == category.Id &&
                   Name == category.Name &&
                   EqualityComparer<ICollection<ProductDto>>.Default.Equals(Products, category.Products);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Products);
        }
    }
}
