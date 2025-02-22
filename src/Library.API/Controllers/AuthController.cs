using AutoMapper;
using Library.API.DTOs;
using Library.API.Extensions;
using Library.API.Services.Interfaces;
using Library.DataAccess.Extensions;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);

            var result = await authService.RegisterAsync(user, HttpContext.RequestAborted).GetResultAsync();

            return result.IsSuccess ? Ok(result.Data) : Conflict(result.ErrorMessage);
        }

        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(UserDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);

            var result = await authService.LoginAsync(user, HttpContext.RequestAborted).GetResultAsync();

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

    }
}
