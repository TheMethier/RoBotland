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
        private readonly DataContext _dataContext;
        public ShoppingCartService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }
        public List<ShoppingCartItem> AddItemToShoppingCart(int productId, List<ShoppingCartItem> shoppingCart)
        {
            var product = _mapper.Map<ProductDto>(_dataContext.Products.Find(productId)) ?? throw new Exception("Product not found");
            if (product.IsAvailable == Enums.Availability.C)
            {
                throw new Exception("Produkt niedostępny!");
            }
            if (shoppingCart.IsNullOrEmpty())
            {
                shoppingCart.Add(new ShoppingCartItem(0, product, 1, product.Price));
                return shoppingCart;
            }
            var identicalItem = shoppingCart.FirstOrDefault(x=>x.Product.Id==product.Id);
            if (identicalItem == null)
            {
                shoppingCart.Add(new ShoppingCartItem(shoppingCart.Count(), product, 1, product.Price));
            }
            else
            {
                if (identicalItem.Quantity <product.Quantity)
                {
                    shoppingCart[identicalItem.Id].Quantity++;
                    shoppingCart[identicalItem.Id].Total += product.Price;
                }
                else
                {
                    throw new Exception("You cannot add unexisting products to your shoppingcart");
                }
            }
            return shoppingCart;
        }

   
        public List<ShoppingCartItem> RemoveItemFromShoppingCart(int productId, List<ShoppingCartItem> shoppingCart)
        {
            if(shoppingCart.IsNullOrEmpty())
                throw new Exception("Empty card");
            var product = _mapper.Map<ProductDto>(_dataContext.Products.Find(productId)) ?? throw new Exception("Product not found");
            var identicalItem = shoppingCart.FirstOrDefault(x => x.Product.Id == productId) ?? throw new Exception("Not found");
            if (identicalItem.Quantity == 1)
            {
                shoppingCart.Remove(identicalItem);
                shoppingCart.ForEach(x => x.Id = shoppingCart.IndexOf(x));
            }
            else
            {
                shoppingCart[identicalItem.Id].Quantity--;
                shoppingCart[identicalItem.Id].Total -= product.Price;
            }
            return shoppingCart;
        }
    }
}
