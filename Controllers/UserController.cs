using Microsoft.AspNetCore.Mvc;
using Library.API.Models;
using Library.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<User>>> GetAllUsers(
            [FromServices] DataContext context
        )
        {

            var users = await context.Users.ToListAsync();

            return Ok(users);

        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Author>> GetUserById(
            int id,
            [FromServices] DataContext context
            )
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<User>> CreateUser(
            [FromBody] User user,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos.", errors = ModelState });

            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao criar o livro.", error = ex.Message });
            }
        }



    }

}