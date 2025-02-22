using Library.DataAccess.Extensions;
using Library.DataAccess.Services.Interfaces;
using Library.Models.Common;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookServie bookService) : ControllerBase
    {
        [HttpGet]
        public async ValueTask<IActionResult> Get(
            [FromQuery] FilterModel filterModel, 
            [FromQuery] BookSortingModel sortingModel, 
            [FromQuery] PaginationModel paginationModel
            )
        {
            var result = await bookService
                                                        .GetAsync(filterModel, sortingModel, paginationModel, HttpContext.RequestAborted)
                                                        .GetResultAsync();

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("book/{title}")]
        public async ValueTask<IActionResult> GetByTitle(string title)
        {
            var result = await bookService
                                                    .GetByTitleAsync(title, HttpContext.RequestAborted)
                                                    .GetResultAsync();

            return result.IsSuccess ?  Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("book")]
        public async ValueTask<IActionResult> Create([FromBody] Book book)
        {
            var result = await bookService
                                                    .CreateAsync(book, HttpContext.RequestAborted)
                                                    .GetResultAsync();

            return result.IsSuccess ? CreatedAtAction(nameof(Create),result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpDelete("book/{title}")]
        public async ValueTask<IActionResult> Delete(string title)
        {
            var result = await bookService.DeleteAsync(title, HttpContext.RequestAborted).GetResultAsync();

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}
