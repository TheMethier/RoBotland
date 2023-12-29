using _RoBotland.Models;

namespace _RoBotland.Interfaces
{
    public interface IShoppingCartService
    {
        List<ShoppingCartItem> AddItemToShoppingCart(int productId, List<ShoppingCartItem> shoppingCart);
        List<ShoppingCartItem> RemoveItemFromShoppingCart(int productId, List<ShoppingCartItem> shoppingCart);
    }
}
