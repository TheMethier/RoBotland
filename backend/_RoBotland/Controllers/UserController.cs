using _RoBotland.Interfaces;
using _RoBotland.Models;
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
            var newUser=_userService.Register(request);
            return Ok(newUser);
        }
        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLoginDto request)
        {
            HttpContext context = HttpContext; // Access the HttpContext property
            var jwt = _userService.Login(request);
            context.Session.SetString("jwt", jwt);
            return Ok(jwt);
        }
        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            HttpContext context = HttpContext; // Access the HttpContext property
            context.Session.SetString("jwt", "");
            return Ok();
        }
    }
}
