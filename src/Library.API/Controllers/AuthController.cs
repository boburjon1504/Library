using Library.DataAccess.Extensions;
using Library.DataAccess.Services.Interfaces;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] User user)
        {
            var result = await authService.RegisterAsync(user, HttpContext.RequestAborted).GetResultAsync();

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("login/{username}")]
        public async ValueTask<IActionResult> Login(string username)
        {
            var result = await authService.LoginAsync(username, HttpContext.RequestAborted).GetResultAsync();

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

    }
}
