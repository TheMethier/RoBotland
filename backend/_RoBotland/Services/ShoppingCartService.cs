using _RoBotland.Helpers;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;

namespace _RoBotland.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IMapper _mapper;
        public ShoppingCartService(IMapper mapper) {
            _mapper = mapper;
        }
        public List<ShoppingCartItem> AddItemToShoppingCart(ProductDto product, List<ShoppingCartItem> shoppingCart)
        {
            if (shoppingCart.IsNullOrEmpty())
            {
                shoppingCart.Add(new ShoppingCartItem(0, product, 1, product.Price));
                return shoppingCart;
            }
            var identicalItem = shoppingCart.FirstOrDefault(x=>x.Product.Id==product.Id);
            if (identicalItem == null)
                shoppingCart.Add(new ShoppingCartItem(shoppingCart.Count(),product,1,product.Price));
            else
            {
                shoppingCart[identicalItem.Id].Quantity++;
                shoppingCart[identicalItem.Id].Total += product.Price;
            }
            return shoppingCart;
        }

   
        public List<ShoppingCartItem> RemoveItemFromShoppingCart(ProductDto product, List<ShoppingCartItem> shoppingCart)
        {
            if(shoppingCart.IsNullOrEmpty())
                throw new Exception("Empty card"); 
            var identicalItem = shoppingCart.FirstOrDefault(x => x.Product.Id == product.Id) ?? throw new Exception("Not found");
            if (identicalItem.Quantity == 1)
                shoppingCart.Remove(identicalItem);
            else
            {
                shoppingCart[identicalItem.Id].Quantity--;
                shoppingCart[identicalItem.Id].Total -= product.Price;
            }
            return shoppingCart;
        }
    }
}
