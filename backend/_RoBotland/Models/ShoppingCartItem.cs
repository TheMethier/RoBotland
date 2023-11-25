namespace _RoBotland.Models
{
    public class ShoppingCartItem
    {
        public ShoppingCartItem()
        {
            Product = new Product();
        }


        public ShoppingCartItem(int id, Product product, int quantity, float total)
        {
            Id = id;
            Product = product;
            Quantity = quantity;
            Total = total;
        }

        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public float Total { get; set; }
    }
}
