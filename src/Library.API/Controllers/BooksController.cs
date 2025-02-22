using AutoMapper;
using Library.API.DTOs.Book;
using Library.API.Extensions;
using Library.API.Services.Interfaces;
using Library.Models.Common;
using Library.Models.Common.Enums;
using Library.Models.Common.ForEntity;
using Library.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IBookServie bookService, IMapper mapper, IRequestUserContext requestUserContext) : ControllerBase
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
            var book = result.Data;
            book.ViewsCount++;

            await bookService.UpdateAsync(book, HttpContext.RequestAborted);

            return result.IsSuccess ?  Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [Authorize]
        [HttpPost("book")]
        public async ValueTask<IActionResult> Create([FromBody] BookDTO bookDTO)
        {
            var userId = requestUserContext.GetRequestUserId();

            var book = mapper.Map<Book>(bookDTO);
            book.UserId = userId;

            var result = await bookService
                                                    .CreateAsync(book, HttpContext.RequestAborted)
                                                    .GetResultAsync();

            return result.IsSuccess ? CreatedAtAction(nameof(Create),result.Data) : BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpPut("book/{id:guid}")]
        public async ValueTask<IActionResult> Update(Guid id, [FromBody] BookDTO bookDTO)
        {
            var exist = await bookService.GetByIdAsync(id, HttpContext.RequestAborted).GetResultAsync();

            if (!exist.IsSuccess)
            {
                return NotFound(exist.ErrorMessage);
            }
            var book = mapper.Map(bookDTO, exist.Data);
            
            var result = await bookService.UpdateAsync(book).GetResultAsync();

            return result.IsSuccess ? Ok(book) : BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpDelete("book/{title}")]
        public async ValueTask<IActionResult> Delete(string title)
        {
            var userId = requestUserContext.GetRequestUserId();

            var result = await bookService.DeleteAsync(title, HttpContext.RequestAborted).GetResultAsync();
            
            if(result.IsSuccess && userId != result.Data!.UserId)
            {
                return Unauthorized("Only admin or book's owner can delete that book");
            }

            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [Authorize(Roles = nameof(Role.Admin))]
        [HttpDelete("by-ids")]
        public async ValueTask<IActionResult> BulkDelete(IList<Guid> ids)
        {
            var result = await bookService.BulkDeleteAsync(ids, HttpContext.RequestAborted);

            return result > 0 ? Ok(result) : NotFound(result);
        }

        [Authorize]
        [HttpDelete("by-titles")]
        public async ValueTask<IActionResult> BulkDelete(IList<string> titles)
        {
            var result = await bookService.BulkDeleteAsync(titles, HttpContext.RequestAborted);

            return result > 0 ? Ok(result) : NotFound(result);
        }
    }
}
