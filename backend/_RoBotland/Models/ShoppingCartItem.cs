namespace _RoBotland.Models
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
            Product = new ProductDto();
        }


        public ShoppingCartItem(int id, ProductDto product, int quantity, float total)
        {
            Id = id;
            Product = product;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
    }
}
