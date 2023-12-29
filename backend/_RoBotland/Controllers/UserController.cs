using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("/register")]
        public IActionResult Register([FromBody] UserRegisterDto request)
        {
            try
            {
                var newUser = _userService.Register(request);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLoginDto request)
        {
            try
            {
                var jwt = _userService.Login(request);
                return Ok(jwt);
            }
            catch (Exception ex) {
            
            return BadRequest(ex.Message);
            }
            

        }
        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("/getHistory")]
        public IActionResult GetHistory()
        {
            var session = HttpContext.Session;
            if (HttpContext.User.Identity == null)
                return NoContent();
            var username = HttpContext.User.Identity.Name
                != null ? HttpContext.User.Identity.Name : string.Empty;
            try
            {
                var history = _userService.GetHistory(username);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
