using InteriorDesigner.Models;
using InteriorDesigner.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InteriorDesigner.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCurrentuser()
        {
            string userId = User.FindFirstValue(ClaimTypes.Name);
            UserModel? newUser = await userService.GetUserByUsernameAsync(userId);
            if(newUser == null)
            {
                return NotFound();
            }

            return Ok(newUser);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(LoginUserModel user)
        {
            IdentityResult result = await userService.RegisterAsync(user);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserModel userToLogin)
        {
            bool success = await userService.LoginAsync(userToLogin);
            if (success)
            {
                string token = userService.GenerateTokenString(userToLogin);
                return Ok(token);
            }
            
            return Forbid();
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
