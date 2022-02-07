using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookrepository;
        public BooksController(IBookRepository bookrepository)
        {
            _bookrepository = bookrepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookrepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooks(int id)
        {
            return await _bookrepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> PostBooks([FromBody] Book book)
        {
            var newBook = await _bookrepository.Create(book);

            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
        }

        [HttpPut]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            await _bookrepository.Update(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var bookToDelete = await _bookrepository.Get(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }
            await _bookrepository.Delete(bookToDelete.Id);
            return NoContent();
        }

    }
}
