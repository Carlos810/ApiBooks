using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;

namespace Repository.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BooksController : Controller
    {
        private readonly BooksDb _context;
        public BooksController(BooksDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>>GetBook(int id)
        {
            var currentBook = await _context.Books.FindAsync(id);
            if (currentBook == null)
                return NotFound();
            return currentBook;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            int returnIfBookWasCreated = await _context.SaveChangesAsync();
            if (returnIfBookWasCreated > 0)
                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> PutBook(int id,Book bookAsParam)
        {
            if (id != bookAsParam.Id)
            {
                return BadRequest();
            }
            else
            {
                var retrievedBook = await _context.Books.FindAsync(id);
                if(retrievedBook == null)
                    return NotFound();

                retrievedBook.Title = bookAsParam.Title ;
                retrievedBook.Author = bookAsParam.Author ;
                retrievedBook.IsAvailable = bookAsParam.IsAvailable ;

                await _context.SaveChangesAsync();
                return Ok(retrievedBook);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
