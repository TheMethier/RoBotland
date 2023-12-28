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

        [HttpPost("/add/{productId:int}")]
        public IActionResult AddProductToShoppingCart(int productId)
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if(context == null)
            {
                context = new List<ShoppingCartItem>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "shoppingcart", context);
            }
            var shoppingCartContent =_shoppingCartService.AddItemToShoppingCart(productId,context);
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
        [HttpDelete("/remove/{productId:int}")]
        public IActionResult RemoveProductFromShoppingCard(int productId) 
        {
            var session = HttpContext.Session;
            var context = SessionHelper.GetObjectFromJson<List<ShoppingCartItem>>(session, "shoppingcart");
            if (context == null)
            {
                return NoContent();
            }
            try
            {
                var shoppingCartContent = _shoppingCartService.RemoveItemFromShoppingCart(productId, context);
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
