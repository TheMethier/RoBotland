using _RoBotland.Helpers;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartController: ControllerBase
    {
        private IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("add/{productId:int}")]
        public IActionResult AddProductToShoppingCart(int productId, [FromBody] List<ShoppingCartItem> shoppingCartItems)
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if(context == null)
            {
                SessionHelper.SetObjectAsJson(session, "shoppingcart", shoppingCartItems);
            }
            try
            {
                var shoppingCartContent = _shoppingCartService.AddItemToShoppingCart(productId, shoppingCartItems);
                SessionHelper.SetObjectAsJson(session, "shoppingcart", shoppingCartContent);
                return Ok(shoppingCartContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("actual")]
        public IActionResult GetShoppingCartItems()
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if(context == null)
            {
                context = new List<ShoppingCartItem>();
                SessionHelper.SetObjectAsJson(session, "shoppingcart", context);
            }
            return Ok(context);
        }
        [HttpDelete("remove/{productId:int}")]
        public IActionResult RemoveProductFromShoppingCard(int productId, [FromBody] List<ShoppingCartItem> shoppingCartItems=null) 
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if(context == null)
            {
                SessionHelper.SetObjectAsJson(session, "shoppingcart", shoppingCartItems);
            }
            try
            {
                var shoppingCartContent = _shoppingCartService.RemoveItemFromShoppingCart(productId, shoppingCartItems);
                SessionHelper.SetObjectAsJson(session, "shoppingcart", shoppingCartContent);
                return Ok(shoppingCartContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
