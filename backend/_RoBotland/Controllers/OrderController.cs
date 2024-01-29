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
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService) 
        { 
            this._orderService = orderService;
        }
        [Authorize]
        [HttpPost("placeOrderByLoggedIn")]
        public IActionResult PlaceOrderByLoggedInUser([FromQuery] OrderOptionsDto orderOptions, [FromBody] List<ShoppingCartItem> shoppingCartItems )
        {
            if (HttpContext.User.Identity == null)
                 return NoContent();
            var username = HttpContext.User.Identity.Name
                != null ? HttpContext.User.Identity.Name : string.Empty; 
            try
            {
                var order = _orderService.PlaceOrderByLoggedInUser(shoppingCartItems, username,orderOptions);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("placeOrderWithoutRegister")]
        public IActionResult PlaceOrderWithoutRegister([FromQuery]UserDetailsDto userDetails, [FromBody] List<ShoppingCartItem> shoppingCartItems)
        {
            try
            {
                var order = _orderService.PlaceOrderWithoutRegister(shoppingCartItems, userDetails);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
