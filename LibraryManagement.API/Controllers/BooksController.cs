using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateBookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = _bookService.CreateBook(dto);
            return CreatedAtAction(nameof(GetById), new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateBookDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _bookService.UpdateBook(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _bookService.DeleteBook(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] BookSearchDTO searchDto)
        {
            var books = _bookService.SearchBooks(searchDto);
            return Ok(books);
        }

        [HttpGet("recommendations/{memberId}")]
        public IActionResult GetRecommendations(int memberId)
        {
            var recommendations = _bookService.GetRecommendationsForMember(memberId);
            return Ok(recommendations);
        }
    }
}
