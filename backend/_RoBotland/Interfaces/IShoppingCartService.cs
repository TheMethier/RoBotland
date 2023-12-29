using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IShoppingCartService
    {
        List<ShoppingCartItem> AddItemToShoppingCart(ProductDto item, List<ShoppingCartItem> shoppingCart);
        List<ShoppingCartItem> RemoveItemFromShoppingCart(ProductDto item, List<ShoppingCartItem> shoppingCart);
    }
}
