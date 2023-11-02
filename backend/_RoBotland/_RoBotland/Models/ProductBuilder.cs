namespace _RoBotland.Models
{
    public class ProductBuilder
    {
        private readonly Product _product;
        public ProductBuilder()
        {
            _product = new Product();
        }
        public void SetName(string name)
        {
            _product.Name = name;
        }
        public void SetImageUrl(string imageUrl)
        {
            _product.ImageUrl = imageUrl;
        }
        public void SetDescription(string description)
        {
            _product.Description = description;
        }
        public void SetPrice(float price)
        {
            _product.Price = price;
        }
        public void SetQuantity(int quantity)
        {
            _product.Quantity = quantity;
        }
        public void SetAvailability(Availability availability)
        {
            _product.IsAvailable = availability;
        }
        public void SetCategories(List<Category> categories)
        {
            _product.Categories=categories;
        }
        public Product Build()
        {
            return _product;
        }
    }
}
