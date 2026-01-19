using Microsoft.AspNetCore.Mvc;
using LibraryManagement.BLL.Services;
using LibraryManagement.Common.DTOs;

namespace LibraryManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
                return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var author = _authorService.CreateAuthor(dto);
            return CreatedAtAction(nameof(GetById), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateAuthorDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _authorService.UpdateAuthor(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _authorService.DeleteAuthor(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
