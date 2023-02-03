using ACME.Data;
using ACME.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ACME.Controllers
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        [Route("signup")]
        [HttpPost]
        public async Task<ActionResult<User>> SignUp(
            [FromBody] User model,
            [FromServices] DataContext context
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(new { message = "Usuário cadastrado com sucesso" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível cadastrar usuário" });
            }
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<User>> Login(
            [FromBody] User model,
            [FromServices] DataContext context
            )
        {

            try
            {
                var userFound = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (userFound == null || userFound.Password != model.Password)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                return Ok("Login realizado com sucesso");
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível fazer login" });
            }
        }
    }
}