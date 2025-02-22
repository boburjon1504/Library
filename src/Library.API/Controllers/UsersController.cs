using Library.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IRequestUserContext requestUserContext) : ControllerBase
    {
        [Authorize]
        [HttpGet("my-books")]
        public async ValueTask<IActionResult> GetMyBooks()
        {
            var id = requestUserContext.GetRequestUserId();

            var books = await userService
                            .Get(u => u.Id == id)
                            .Include(u => u.Books)
                            .Select(x => x.Books)
                            .FirstOrDefaultAsync();

            return Ok(books);
        }

    }
}
