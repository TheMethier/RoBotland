using _RoBotland.Enums;

namespace _RoBotland.Models
{
    public class ProductBuilder
    {
        private ProductDto _product;
        public ProductBuilder()
        {
            _product = new ProductDto();
        }
        public ProductBuilder SetName(string name)
        {
            _product.Name = name;
            return this;
        }
        public ProductBuilder SetImageUrl(string imageUrl)
        {
            _product.ImageUrl = imageUrl;
            return this;
        }
        public ProductBuilder SetDescription(string description)
        {
            _product.Description = description;
            return this;

        }
        public ProductBuilder SetPrice(float price)
        {
            _product.Price = price;
            return this;
        }
        public ProductBuilder SetQuantity(int quantity)
        {
            _product.Quantity = quantity;
            return this;
        }
        public ProductBuilder SetAvailability(Availability availability)
        {
            _product.IsAvailable = availability;
            return this;
        }
        public ProductBuilder SetCategories(List<Category> categories)
        {
            _product.Categories=categories;
            return this;
        }
        public ProductDto Build()
        {
            return _product;
        }
    }
}
