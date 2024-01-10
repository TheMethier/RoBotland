﻿using _RoBotland.Interfaces;
using _RoBotland.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register")]
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
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto request)
        {
            try
            {
                var jwt = _userService.Login(request);
                HttpContext.Response.Cookies.Append("token", jwt.Token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(15),
                    HttpOnly = false,
                    IsEssential = true,
                    Secure = true
                });
                
                return Ok(jwt);
            }
            catch (Exception ex) {
            
            return BadRequest(ex.Message);
            }
            

        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("identify")]
        public IActionResult GetUsername()
        {

            var username = new
            { username =
                HttpContext.User.Identity != null ?
                        HttpContext.User.Identity.Name != null ?
                            HttpContext.User.Identity.Name :
                            string.Empty
                        : string.Empty,
                role = HttpContext.User.HasClaim(ClaimTypes.Role, "USER") ? "USER" : "ADMIN"
            };
            
            return Ok(JsonConvert.SerializeObject(username));
        }
        [Authorize]
        [HttpGet("getAccountBalance")]
        public IActionResult GetAccountBalance()
        {

            var session = HttpContext.Session;
            var username = HttpContext.User.Identity != null ?
                HttpContext.User.Identity.Name !=null ? 
                    HttpContext.User.Identity.Name :
                    string.Empty
                : string.Empty;
            try
            {
                var accountBalance = _userService.GetAccountBalance(username);
                return Ok(accountBalance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpPut("depositToAccount")]
        public IActionResult DepositToAccount(float amount)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized("User Not Authenticated");
            }
            var session = HttpContext.Session;
            if (HttpContext.User.Identity == null)
                return NoContent();
            var username = HttpContext.User.Identity.Name
                != null ? HttpContext.User.Identity.Name : string.Empty;
            try
            {
                _userService.DepositToAccount(username, amount);
                return Ok("DepositToAccount");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Authorize]
        [HttpPut("withdrawFromAccount")]
        public IActionResult WithdrawFromAccount(float amount)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Unauthorized("User Not Authenticated");
            }
            var session = HttpContext.Session;
            if (HttpContext.User.Identity == null)
                return NoContent();
            var username = HttpContext.User.Identity.Name
                != null ? HttpContext.User.Identity.Name : string.Empty;
            try
            {
                _userService.WithdrawFromAccount(username, amount);
                return Ok("WithdrawFromAccount");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("getHistory")]
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
