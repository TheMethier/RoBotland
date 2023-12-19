using _RoBotland.Interfaces;
using _RoBotland.Migrations;
using _RoBotland.Models;
using _RoBotland.Services;
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
        [HttpGet("/getAccountBalance")]
        public IActionResult GetAccountBalance(User user) {
            var accountBalance = _userService.GetAccountBalance(user.Id);
            return Ok(accountBalance);
        }
        [HttpPut("/depositToAccount")]
        public IActionResult DepositToAccount(Guid userId, float amount) {
            _userService.DepositToAccount(userId, amount);
            return Ok();
        }
        [HttpPut("/withdrawFromAccount")]
        public IActionResult WithdrawFromAccount(Guid userId, float amount) {
            _userService.WithdrawFromAccount(userId, amount);
            return Ok();
        }
    }
}
