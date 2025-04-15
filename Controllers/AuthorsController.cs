using Microsoft.AspNetCore.Mvc;
using Library.API.Models;
using Library.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{

    [ApiController]
    [Route("authors")]
    public class AuhtorsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Author>>> GetAuthors(
            [FromServices] DataContext context
        )
        {
            var authors = await context.Authors.ToListAsync();
            return Ok(authors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Author>> GetAuthorById(
            int id,
            [FromServices] DataContext context
            )
        {
            var author = await context.Authors.FindAsync(id);

            if (author == null)
                return NotFound("Autor não encontrado.");

            return Ok(author);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Author>> DeteleAuthorById(
            int id,
            [FromServices] DataContext context
            )
        {
            try
            {
                var author = await context.Authors.FindAsync(id);

                if (author == null)
                    return NotFound("Autor não encontrado");

                context.Authors.Remove(author);
                await context.SaveChangesAsync();

                return Ok(new { message = "Autor removido com sucesso" });
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Author>> UpdateAuthor(
            int id,
            [FromBody] Author author,
            [FromServices] DataContext context
        )
        {
            if (id != author.Id)
                return NotFound("Autor não encontrado");

            var existingAuthor = await context.Authors.FindAsync(id);

            if (existingAuthor == null)
                return NotFound("Autor não encontrado");

            existingAuthor.Name = author.Name;

            await context.SaveChangesAsync();

            return Ok(new { message = "O nome do autor foi alterado com sucesso" });

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Author>> CreateAuthor(
            [FromBody] Author author,
            [FromServices] DataContext context
            )
        {
            if(!ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos.", errors = ModelState });

            try
            {
                context.Authors.Add(author);
                await context.SaveChangesAsync();

                return Ok(author);
            }
            catch (Exception ex)
            {
               
                return BadRequest(new { message = "Erro ao criar um novo autor.", error = ex.Message });
            }
        }
    }

}