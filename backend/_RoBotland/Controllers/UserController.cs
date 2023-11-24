using Microsoft.AspNetCore.Mvc;

namespace _RoBotland.Controllers
{
    [Route("/api/v1/user/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IActionResult Register()
        {
            return Ok();
        }
    }
}
