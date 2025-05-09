using Example_C_2.Context;
using Example_C_2.Dto;
using Example_C_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Example_C_2.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationContextDb _context;
        public BookController(ApplicationContextDb contextDb) {
            _context = contextDb;
        }

        [HttpPost("AddNewBook")]
        public async Task<IActionResult> AddNewBook([FromBody] BookRequestDto request)
        {
            ComicBook newBook = new ComicBook();
            newBook.Title = request.Title;
            newBook.Author = request.Author;
            newBook.PricePerDay = request.PricePerDay;
            await _context.ComicBooks.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return Ok("Add new book success.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.ComicBooks.ToListAsync());
        }

        [HttpDelete("DeleteBook/{bookId}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookId)
        {
            var book = await _context.ComicBooks.FindAsync(bookId);
            _context.ComicBooks.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Delete book successful.");
        }


        [HttpPatch("UpdateBook/{bookId}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int bookId, [FromBody] BookRequestDto request)
        {
            var book = await _context.ComicBooks.FindAsync(bookId);
            book.Title = request.Title;
            book.Author = request.Author;
            book.PricePerDay = request.PricePerDay;

            await _context.SaveChangesAsync();
            return Ok("Update successful.");
        }

        
    }
}
