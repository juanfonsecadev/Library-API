using Microsoft.AspNetCore.Mvc;
using Library.API.Models;
using Library.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{

    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Book>>> getAllBooks(
        [FromServices] DataContext context
        )
        {
            var books = await context.Book.ToListAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Book>> getBookById(
            int id,
            [FromServices] DataContext context
            )
        {
            var book = await context.Book.FindAsync(id);

            if (book == null)
                return NotFound("Livro não encontrado");

            return Ok(book);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Book>> updateBook(
            int id,
            [FromBody] Book book,
            [FromServices] DataContext context)
        {

            if (id != book.Id)
                return NotFound("Livro não encontrado");

            var existingBook = await context.Book.FindAsync(id);

            if (existingBook == null)
                return NotFound("Livro não encontrado.");


            existingBook.Title = book.Title;
            existingBook.PublishedYear = book.PublishedYear;
            existingBook.UpdatedAt = DateTime.Now;

            await context.SaveChangesAsync();

            return Ok(existingBook);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Book>> Post(
            [FromBody] Book book,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(new { message = "Dados inválidos.", errors = ModelState });
            }

            try
            {
                book.CreatedAt = DateTime.Now;
                book.UpdatedAt = DateTime.Now;

                context.Book.Add(book);
                await context.SaveChangesAsync();

                return Ok(book);
            }
            catch (Exception ex)
            {
               
                return BadRequest(new { message = "Erro ao criar o livro.", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            try
            {
                var book = await context.Book.FindAsync(id);

                if (book == null)
                    return NotFound("Livro não encontrado");

                context.Book.Remove(book);
                await context.SaveChangesAsync();

                return Ok(new { message = "Livro removido com sucesso! " });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao deletar o livro.", error = ex.Message });
            }
        }

    }
}