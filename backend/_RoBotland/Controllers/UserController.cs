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
    }
}
