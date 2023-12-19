using _RoBotland.Helpers;
using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/orders/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        { 
            this._orderService = orderService;
        }
        [Authorize]
        [HttpPost("/placeOrderByLoggedIn")]
        public IActionResult PlaceOrderByLoggedInUser([FromBody] OrderOptionsDto orderOptions )
        {
            var session = HttpContext.Session;
            if (HttpContext.User.Identity == null)
                 return NoContent();
            var username = HttpContext.User.Identity.Name
                != null ? HttpContext.User.Identity.Name : string.Empty;

            try
            {
                var order = _orderService.PlaceOrderByLoggedInUser(session, username,orderOptions);
                SessionHelper.SetObjectAsJson(session, "shoppingcart", new List<ShoppingCartItem>());
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);//dodaj ify na errory
            }
        }
        [HttpPost("/placeOrderWithoutRegister")]
        public IActionResult PlaceOrderWithoutRegister([FromBody]UserDetailsDto userDetails)
        //Test it
        {
            var session = HttpContext.Session;
            try
            {
                var order = _orderService.PlaceOrderWithoutRegister(session, userDetails);
                SessionHelper.SetObjectAsJson(session, "shoppingcart", new List<ShoppingCartItem>());
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);//dodaj ify na errory
            }
        }
    }
}
