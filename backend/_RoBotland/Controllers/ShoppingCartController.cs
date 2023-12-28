using _RoBotland.Helpers;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/shoppingCart/[controller]")]
    [ApiController]
    public class ShoppingCartController: ControllerBase
    {
        private IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("/add")]
        public IActionResult AddProductToShoppingCart([FromBody] ProductDto product)
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if(context == null)
            {
                context = new List<ShoppingCartItem>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingcart", context);
            }
            var shoppingCartContent =_shoppingCartService.AddItemToShoppingCart(product,context);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingcart", shoppingCartContent);
            return Ok(shoppingCartContent);
        }
        [HttpGet("/actual")]
        public IActionResult GetShoppingCartItems()
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if (context == null) return NoContent();
            return Ok(context);
        }
        [HttpDelete("/remove")]
        public IActionResult RemoveProductFromShoppingCard([FromBody] ProductDto product) 
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if (context == null)
            {
                return NoContent();
            }
            try
            {
                var shoppingCartContent = _shoppingCartService.RemoveItemFromShoppingCart(product, context);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingcart", shoppingCartContent);
                return Ok(shoppingCartContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);//dodaj ify na errory
            }

        }


    }
}
